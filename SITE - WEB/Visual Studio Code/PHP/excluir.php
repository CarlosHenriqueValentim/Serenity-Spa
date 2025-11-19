<?php
include __DIR__ . "/database.php";

if (!isset($_GET['id']) || empty($_GET['id'])) {
    echo "<p style='color:red'>Erro: nenhum ID informado.</p>";
    echo "<a href='painel.php'>Voltar</a>";
    exit;
}

$id = intval($_GET['id']);

try {
    $verificar = $conn->prepare("SELECT * FROM agendamentos WHERE codigo_agendamento = :id LIMIT 1");
    $verificar->bindParam(":id", $id, PDO::PARAM_INT);
    $verificar->execute();

    if ($verificar->rowCount() === 0) {
        echo "<p style='color:red'>Agendamento não encontrado.</p>";
        echo "<a href='painel.php'>Voltar</a>";
        exit;
    }
    
    $delete = $conn->prepare("DELETE FROM agendamentos WHERE codigo_agendamento = :id");
    $delete->bindParam(":id", $id, PDO::PARAM_INT);
    $delete->execute();

    echo "<script>alert('Agendamento cancelado com sucesso!'); window.location='painel.php';</script>";
    exit;

} catch (PDOException $e) {
    echo "<p style='color:red'>Erro ao cancelar: " . htmlspecialchars($e->getMessage()) . "</p>";
    echo "<a href='painel.php'>Voltar</a>";
    exit;
}
