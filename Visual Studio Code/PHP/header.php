<?php
session_start();
?>
<!DOCTYPE html>
<html lang="pt-br">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Serenity Spa</title>
    <link rel="stylesheet" href="css/style.css">
</head>
<body>
<header>
    <h1>Serenity Spa</h1>
    <?php if(isset($_SESSION['usuario'])): ?>
        <p>Bem-vindo, <?php echo $_SESSION['usuario']; ?> | <a href="logout.php">Sair</a></p>
    <?php endif; ?>
</header>
<main>
