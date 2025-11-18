<?php
// PHP/includes/header.php
// Aqui iniciamos a sessão apenas neste arquivo (único lugar)
if (session_status() == PHP_SESSION_NONE) session_start();
?>
<!DOCTYPE html>
<html lang="pt-br">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>SPA Serenity</title>
  <link rel="stylesheet" href="../CSS/style.css">
  <script src="../JAVA SCRIPT/script.js"></script>
</head>
<body class="CorpoPadrão">
<header>
  <div class="A">
    <img src="../IMAGENS/Serenity.jpeg" alt="Logo Serenity" class="A-img">
    SPA Serenity
  </div>
  <nav>
    <?php if(!empty($_SESSION['usuario'])): ?>
      <a href="painel.php">Painel</a>
      <a href="form-agendar.php">Agendar</a>
      <a href="logout.php">Sair (<?php echo htmlspecialchars($_SESSION['usuario']['nome']); ?>)</a>
    <?php else: ?>
      <a href="index.php">Entrar</a>
      <a href="form-cad.php">Cadastrar</a>
      <a href="form-agendar.php">Agendar</a>
      <a href="../HTML/index.html">Voltar</a>
    <?php endif; ?>
  </nav>
</header>
<main>
