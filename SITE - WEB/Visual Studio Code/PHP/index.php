<?php
include __DIR__.'/includes/header.php';
session_start();

if(isset($_SESSION['usuario'])){
    header('Location:painel.php');
    exit();
}
?>

<main>
    <section class="H">
        <div class="I">
            <h1>Digite abaixo</h1>
            <form action="logar.php" method="post">
                <label for="login">Email</label>
                <input type="text" id="login" name="login" placeholder="Exemplo: Email@gmail.com" maxlength="100" required>
                <label for="senha">Senha</label>
                <input type="password" id="senha" name="senha" maxlength="100" required>
                <input type="submit" value="Entrar" class="btn">
            </form>
            <p class="msg">Ainda não agendou? <a href="form-cad.php">Agendar Agora</a></p>
        </div>
    </section>
</main>

<?php
include __DIR__.'/includes/footer.php';
?>
