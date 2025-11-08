<?php
include __DIR__.'/database.php';

$nome = $_POST['nome_cliente'] ?? '';
$telefone = $_POST['telefone_cliente'] ?? '';
$servico = $_POST['servico'] ?? '';
$data = $_POST['data_agendamento'] ?? '';
$hora = $_POST['hora_agendamento'] ?? '';
$obs = $_POST['obs'] ?? '';

try {
    $stmt = $conn->prepare("INSERT INTO agendamentos_simples (nome, telefone, servico, data, hora, obs) VALUES (?, ?, ?, ?, ?, ?)");
    $stmt->execute([$nome, $telefone, $servico, $data, $hora, $obs]);

    echo "<p>Agendamento realizado com sucesso!</p>";
} catch(PDOException $e){
    echo "Erro: " . $e->getMessage();
}
?>
<p><a href="form-cad.php">Voltar</a></p>
