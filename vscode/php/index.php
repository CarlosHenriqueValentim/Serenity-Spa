<?php
include __DIR__.'/includes/header.php';
session_start();

if(isset($_SESSION['usuario'])){
    header('Location:painel.php');
    exit();
}
?>

<main>
    <div class="login-container">
        <h2>Login</h2>
        <form action="logar.php" method="post">
            <label for="login">Login (Email):</label>
            <input type="text" id="login" name="login" maxlength="100" required>
            <label for="senha">Senha:</label>
            <input type="password" id="senha" name="senha" maxlength="100" required>
            <input type="submit" value="Entrar" class="btn">
        </form>
        <p class="msg">Não tem conta? <a href="form-cad.php">Cadastrar</a></p>
    </div>
</main>

<?php
include __DIR__.'/includes/footer.php';
?>
