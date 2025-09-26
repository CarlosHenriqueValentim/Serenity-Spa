<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <link rel="icon" href="Serenity.jpeg" type="image/png">
    <title>SPA Serenity Login</title>
    <link rel="stylesheet" href="style.css">
</head>
<body>
<header>
    <div class="logo">
        <img src="Serenity.jpeg" alt="SPA Serenity" class="logo-img">
        SPA Serenity
    </div>
    <nav>
        <a href="index.php" class="<?= basename($_SERVER['PHP_SELF'])=='index.php'?'active':'' ?>">Login</a>
        <?php if(isset($_SESSION['nome'])): ?>
            <a href="agendar.php" class="<?= basename($_SERVER['PHP_SELF'])=='agendar.php'?'active':'' ?>">Agendar</a>
            <a href="logout.php">Sair</a>
        <?php endif; ?>
    </nav>
</header>
