<?php
include __DIR__ . "/database.php";

// Verificar se os campos existem
if(!isset(
    $_POST['nome'],
    $_POST['email'],
    $_POST['zap'],
    $_POST['servico'],
    $_POST['data_agendamento'],
    $_POST['hora'],
    $_POST['obs']
)){
    header('Location: form-agendar.php');
    die();
}

$nome   = $_POST['nome'];
$email  = $_POST['email'];
$zap    = $_POST['zap'];
$serv   = $_POST['servico'];
$data   = $_POST['data_agendamento'];
$hora   = $_POST['hora'];
$obs    = $_POST['obs'];

// Se o professor pediu ID de usuário obrigatório, você define aqui
// Caso não tenha login para esse formulário, deixar ID fixo = 1
$id_user = 1;

try{
    // Inserção no padrão do professor
    $stmt = $conn->prepare("
        INSERT INTO agenda
        (id_user, nome_ag, desc_ag, data_ini_ag, dia_ag, email, zap, hora, obs, servico)
        VALUES (:id_user, :nome, :desc, :data, 1, :email, :zap, :hora, :obs, :servico);
    ");

    // Descrição junta serviço + observação
    $descricao = $serv . ' - ' . $obs;

    $stmt->bindParam(':id_user', $id_user);
    $stmt->bindParam(':nome', $nome);
    $stmt->bindParam(':desc', $descricao);
    $stmt->bindParam(':data', $data);
    $stmt->bindParam(':email', $email);
    $stmt->bindParam(':zap', $zap);
    $stmt->bindParam(':hora', $hora);
    $stmt->bindParam(':obs', $obs);
    $stmt->bindParam(':servico', $serv);

    $stmt->execute();

    header('Location: painel.php');
} catch(PDOException $e){
    echo "ERRO: " . $e->getMessage();
}
?>
