<?php
$usuarioLogado = $_SESSION['usuario'] ?? null;
$arquivoAtual  = basename($_SERVER['PHP_SELF']);
$tipoUsuario   = $usuarioLogado['tipo'] ?? null;
$nomeUsuario   = $usuarioLogado['nome'] ?? null;
$cargoUsuario  = $usuarioLogado['cargo'] ?? null;
?>

<!DOCTYPE html>
<html lang="pt-br">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>SPA Serenity</title>
    <link rel="stylesheet" href="../CSS/style.css">
    <script src="../JAVA%20SCRIPT/script.js" defer></script>
</head>
<body class="CorpoPadrão">

<header>
    <div class="A">
        <img src="../IMAGENS/Serenity.jpeg" alt="Logo Serenity" class="A-img">
        SPA Serenity
    </div>

    <nav>
        <?php if (!$usuarioLogado): ?>
            <?php if ($arquivoAtual === 'index.php'): ?>
                <a href="../HTML/index.html">Voltar</a>
            <?php elseif ($arquivoAtual === 'form-cad.php'): ?>
                <a href="index.php">Entrar</a>
            <?php else: ?>
                <a href="painel.php">Meus Agendamentos</a>
                <a href="form-agendar.php">Agendar</a>
                <a href="../HTML/index.html">Página Inicial</a>
                <a href="logout.php">Sair da Conta</a>
            <?php endif; ?>
        <?php else: ?>
            <div>
                <?php if ($nomeUsuario): ?>
                <?php endif; ?>

                <?php if ($tipoUsuario === 'cliente'): ?>
                    <?php if ($arquivoAtual === 'painel.php'): ?>
                        <a href="form-agendar.php" class="btn-link">Agendar</a>
                        <a href="../HTML/index.html" class="btn-link">Página Inicial</a>
                        <a href="logout.php" class="btn-link">Sair da Conta</a>
                    <?php elseif ($arquivoAtual === 'form-agendar.php'): ?>
                        <a href="painel.php" class="btn-link">Meus Agendamentos</a>
                    <?php else: ?>
                        <a href="form-agendar.php" class="btn-link">Agendar</a>
                        <a href="painel.php" class="btn-link">Meus Agendamentos</a>
                        <a href="../HTML/index.html" class="btn-link">Página Inicial</a>
                        <a href="logout.php" class="btn-link">Sair da Conta</a>
                    <?php endif; ?>

                <?php elseif ($tipoUsuario === 'funcionario'): ?>
                    <?php if ($arquivoAtual === 'painel-admin.php'): ?>
                        <a href="../HTML/index.html" class="btn-link">Página Inicial</a>
                        <a href="logout.php" class="btn-link">Sair da Conta</a>
                    <?php else: ?>
                        <a href="painel-admin.php" class="btn-link">Painel Administrativo</a>
                        <a href="../HTML/index.html" class="btn-link">Página Inicial</a>
                        <a href="logout.php" class="btn-link">Sair da Conta</a>
                    <?php endif; ?>
                <?php endif; ?>
            </div>
        <?php endif; ?>
    </nav>
</header>
<main>
