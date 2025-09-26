<?php
session_start();
include __DIR__ . "/header.php";

if(isset($_SESSION['nome'])) {
    header("Location: agendar.php");
    exit;
}

if(isset($_GET['erro'])) {
    echo "<p class='mensagem'>";
    switch($_GET['erro']){
        case 1: echo "Usuário ou senha inválidos."; break;
        case 2: echo "Senhas não conferem."; break;
        case 3: echo "Preencha todos os campos."; break;
    }
    echo "</p>";
}
?>

<h2>Login ou Cadastro</h2>

<form action="processa_login.php" method="POST" class="form-container">
    <label>Nome:</label>
    <input type="text" name="nome" required>

    <label>Email:</label>
    <input type="email" name="email" required>

    <label>Senha:</label>
    <input type="password" name="senha" required>

    <label>Confirmar Senha (apenas para cadastro):</label>
    <input type="password" name="confirmar_senha">

    <button type="submit">Entrar / Cadastrar</button>
</form>

<?php include __DIR__ . "/footer.php"; ?>
