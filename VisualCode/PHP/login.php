<?php include __DIR__."/includes/header.php"; ?>

<h2>Login - Serenity Spa</h2>
<form action="logar.php" method="post">
    <label>Login:</label>
    <input type="text" name="login" maxlength="100" required><br>

    <label>Senha:</label>
    <input type="password" name="senha" maxlength="100" required><br>

    <input type="submit" value="Login">
</form>

<p>Não possui conta? <a href="form-cad.php">Cadastre-se</a></p>

<?php include __DIR__."/includes/footer.php"; ?>
