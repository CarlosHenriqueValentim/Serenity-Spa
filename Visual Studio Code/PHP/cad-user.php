<?php
include __DIR__.'/database.php';

// Verifica se os campos foram enviados
if(!isset($_POST['nome'], $_POST['zap'], $_POST['servico'], $_POST['data'], $_POST['hora'])){
    header('Location: form-cad.php');
    exit();
}

// Captura os dados do formulário
$nome = $_POST['nome'];
$zap = $_POST['zap'];
$servico = $_POST['servico'];
$data = $_POST['data'];
$hora = $_POST['hora'];
$obs = $_POST['obs'] ?? '';

try {
    $stmt = $conn->prepare("INSERT INTO agendamentos (nome, zap, servico, data, hora, obs) VALUES (?, ?, ?, ?, ?, ?)");
    $stmt->execute([$nome, $zap, $servico, $data, $hora, $obs]);
    echo "<p>Agendamento realizado com sucesso!</p>";
} catch(PDOException $e){
    echo "Erro: ".$e->getMessage();
}
?>
<p><a href="form-cad.php">Voltar</a></p>
