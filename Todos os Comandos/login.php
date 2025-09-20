<?php
session_start();

$nome        = $_POST['nome'] ?? '';
$whatsapp    = $_POST['zap'] ?? '';
$servico     = $_POST['servico'] ?? '';
$data        = $_POST['data'] ?? '';
$hora        = $_POST['hora'] ?? '';
$obs         = $_POST['obs'] ?? '';
$senha       = $_POST['senha'] ?? '';
$confirma    = $_POST['confirmar_senha'] ?? '';

if (empty($nome) || empty($whatsapp) || empty($servico) || empty($data) || empty($hora) || empty($senha) || empty($confirma)) {
    header("Location: index.php?erro=1");
    exit;
}

if ($senha !== $confirma) {
    header("Location: index.php?erro=2");
    exit;
}

$dataHoraSelecionada = strtotime("$data $hora");
if ($dataHoraSelecionada < strtotime("today")) {
    header("Location: index.php?erro=3");
    exit;
}

$_SESSION['nome']     = $nome;
$_SESSION['whatsapp'] = $whatsapp;
$_SESSION['servico']  = $servico;
$_SESSION['data']     = $data;
$_SESSION['hora']     = $hora;
$_SESSION['obs']      = $obs;
?>

<?php include __DIR__ . "/header.php"; ?>

<h2>✅ Agendamento Confirmado!</h2>
<p><strong>Nome:</strong> <?= htmlspecialchars($nome) ?></p>
<p><strong>WhatsApp:</strong> <?= htmlspecialchars($whatsapp) ?></p>
<p><strong>Serviço:</strong> <?= htmlspecialchars($servico) ?></p>
<p><strong>Data:</strong> <?= date("d/m/Y", strtotime($data)) ?></p>
<p><strong>Horário:</strong> <?= htmlspecialchars($hora) ?></p>
<p><strong>Observações:</strong> <?= nl2br(htmlspecialchars($obs)) ?></p>

<?php include __DIR__ . "/footer.php"; ?>
