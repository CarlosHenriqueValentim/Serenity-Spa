<?php
include __DIR__."/database.php";
session_start();

if(!isset($_POST['login'], $_POST['senha'])){
    header("Location: login.php");
    die();
}

$login = $_POST['login'];
$senha = $_POST['senha'];

$sql = "SELECT * FROM clientes WHERE login = :login";
$stmt = $conn->prepare($sql);
$stmt->bindParam(':login', $login);
$stmt->execute();

$usuario = $stmt->fetch(PDO::FETCH_ASSOC);

if($usuario && password_verify($senha, $usuario['senha'])){
    $_SESSION['usuario'] = $usuario['nome'];
    header("Location: painel.php");
} else {
    echo "Login ou senha incorretos! <a href='login.php'>Tentar novamente</a>";
}
?>
