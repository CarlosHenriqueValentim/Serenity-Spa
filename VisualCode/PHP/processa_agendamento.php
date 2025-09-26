<?php
session_start();


if (!isset($_SESSION['nome'])) {
    header("Location: index.php");
    exit;
}


$servico = trim($_POST['servico'] ?? '');
$data = trim($_POST['data'] ?? '');
$hora = trim($_POST['hora'] ?? '');
$obs = trim($_POST['obs'] ?? '');


if (empty($servico) || empty($data) || empty($hora)) {
    $_SESSION['mensagem'] = "⚠️ Preencha todos os campos obrigatórios.";
    header("Location: agendar.php");
    exit;
}

if ($data < date('Y-m-d')) {
    $_SESSION['mensagem'] = "⚠️ A data não pode ser no passado.";
    header("Location: agendar.php");
    exit;
}


$arquivo = __DIR__ . "/agendamentos.csv";


$linha = [$_SESSION['nome'], $_SESSION['email'], $servico, $data, $hora, $obs];


$arquivo_csv = fopen($arquivo, 'a');
if ($arquivo_csv === false) {
    die("❌ Erro ao abrir o arquivo de agendamentos.");
}

fputcsv($arquivo_csv, $linha);
fclose($arquivo_csv);


$_SESSION['mensagem'] = "✅ Agendamento salvo com sucesso!";
header("Location: agendar.php");
exit;
