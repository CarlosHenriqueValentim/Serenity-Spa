<?php
session_start();
include __DIR__ . "/database.php";

if (!isset($_POST['nome'], $_POST['email'], $_POST['telefone'], $_POST['servico'], $_POST['data_agendamento'], $_POST['hora'])) {
    header('Location: form-agendar.php');
    exit;
}

$nome     = trim($_POST['nome']);
$email    = trim($_POST['email']);
$telefone = trim($_POST['telefone']);
$serv     = trim($_POST['servico']);
$data     = $_POST['data_agendamento']; 
$hora     = $_POST['hora'];            
$obs      = isset($_POST['obs']) ? trim($_POST['obs']) : '';

$codigo_empresa = 1;
$codigo_funcionario = 1;

$codigo_cliente = 1;
if (!empty($_SESSION['clientes']['codigo'])) {
    $codigo_cliente = (int)$_SESSION['clientes']['codigo'];
} elseif (!empty($_SESSION['clientes']['codigo_cliente'])) {
    $codigo_cliente = (int)$_SESSION['clientes']['codigo_cliente'];
} elseif (!empty($_SESSION['cliente']['id'])) {
    $codigo_cliente = (int)$_SESSION['cliente']['id'];
} elseif (!empty($_SESSION['usuario']['id'])) {
    $codigo_cliente = (int)$_SESSION['usuario']['id'];
}

try {
    $q = $conn->prepare("SELECT codigo_servico, duracao_min_servico FROM servicos WHERE nome_servico = :nome LIMIT 1");
    $q->bindValue(':nome', $serv);
    $q->execute();
    $svc = $q->fetch(PDO::FETCH_ASSOC);

    if ($svc) {
        $codigo_servico = (int)$svc['codigo_servico'];
        $dur_min = (int)$svc['duracao_min_servico'];
    } else {
        $svc2 = $conn->query("SELECT codigo_servico, duracao_min_servico FROM servicos LIMIT 1")->fetch(PDO::FETCH_ASSOC);
        if ($svc2) {
            $codigo_servico = (int)$svc2['codigo_servico'];
            $dur_min = (int)$svc2['duracao_min_servico'];
        } else {
            $codigo_servico = 1;
            $dur_min = 30;
        }
    }
} catch (PDOException $e) {
    echo "Erro ao buscar serviço: " . htmlspecialchars($e->getMessage());
    exit;
}

function minutesToTime($min) {
    $h = floor($min / 60);
    $m = $min % 60;
    return sprintf('%02d:%02d:00', $h, $m);
}
$duracao_time = minutesToTime($dur_min);

$status = 'agendado';

try {
    $stmt = $conn->prepare("
        INSERT INTO agendamentos
        (codigo_empresa, codigo_clientes, codigo_funcionario, codigo_servico, data, duracao_agendamento, status)
        VALUES
        (:codigo_empresa, :codigo_clientes, :codigo_funcionario, :codigo_servico, :data, :duracao_agendamento, :status)
    ");

    $stmt->bindValue(':codigo_empresa', $codigo_empresa, PDO::PARAM_INT);
    $stmt->bindValue(':codigo_clientes', $codigo_cliente, PDO::PARAM_INT);
    $stmt->bindValue(':codigo_funcionario', $codigo_funcionario, PDO::PARAM_INT);
    $stmt->bindValue(':codigo_servico', $codigo_servico, PDO::PARAM_INT);
    $stmt->bindValue(':data', $data);
    $stmt->bindValue(':duracao_agendamento', $duracao_time);
    $stmt->bindValue(':status', $status);

    $stmt->execute();

    header('Location: painel.php');
    exit;

} catch (PDOException $e) {
    echo "ERRO: " . htmlspecialchars($e->getMessage());
    exit;
}
