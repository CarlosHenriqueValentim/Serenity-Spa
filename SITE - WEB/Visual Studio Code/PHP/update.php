<?php
include __DIR__ . '/database.php';

if (!isset($_POST['id'], $_POST['servico'], $_POST['data'], $_POST['duracao_agendamento'], $_POST['status'])) {
    header('Location: painel.php');
    exit;
}

try {
    $id      = $_POST['id'];
    $servico = $_POST['servico']; 
    $data    = $_POST['data'];
    $dur     = $_POST['duracao_agendamento'];
    $status  = $_POST['status'];

    $stmt = $conn->prepare("
        UPDATE agendamentos
        SET codigo_servico = :servico,
            data = :data,
            duracao_agendamento = :duracao,
            status = :status
        WHERE codigo_agendamento = :id
    ");

    $stmt->bindParam(':servico', $servico, PDO::PARAM_INT);
    $stmt->bindParam(':data', $data);
    $stmt->bindParam(':duracao', $dur);
    $stmt->bindParam(':status', $status);
    $stmt->bindParam(':id', $id, PDO::PARAM_INT);

    $stmt->execute();

    header('Location: painel.php');
    exit;

} catch (PDOException $e) {
    echo "ERRO: " . $e->getMessage();
}
