<?php include __DIR__."/includes/header.php"; ?>

<h2>Cadastro de Cliente</h2>
<form action="cad-user.php" method="post">
    <label>Nome:</label>
    <input type="text" name="nome" maxlength="100" required><br>

    <label>RG:</label>
    <input type="text" name="rg" maxlength="9" required><br>

    <label>Data de Nascimento:</label>
    <input type="date" name="data_nasc" required><br>

    <label>Login:</label>
    <input type="text" name="login" maxlength="100" required><br>

    <label>Senha:</label>
    <input type="password" name="senha" maxlength="100" required><br>

    <input type="submit" value="Cadastrar">
    <input type="reset" value="Limpar">
</form>

<p>Já possui conta? <a href="login.php">Login</a></p>

<?php include __DIR__."/includes/footer.php"; ?>
