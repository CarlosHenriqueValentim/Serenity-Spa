<?php
include __DIR__."/database.php";

if(!isset($_POST['nome'], $_POST['rg'], $_POST['data_nasc'], $_POST['login'], $_POST['senha'])){
    header("Location: form-cad.php");
    die();
}

$nome = $_POST['nome'];
$rg = $_POST['rg'];
$data_nasc = $_POST['data_nasc'];
$login = $_POST['login'];
$senha = password_hash($_POST['senha'], PASSWORD_DEFAULT);

$sql = "INSERT INTO clientes (nome, rg, data_nasc, login, senha) VALUES (:nome, :rg, :data_nasc, :login, :senha)";
$stmt = $conn->prepare($sql);

$stmt->bindParam(':nome', $nome);
$stmt->bindParam(':rg', $rg);
$stmt->bindParam(':data_nasc', $data_nasc);
$stmt->bindParam(':login', $login);
$stmt->bindParam(':senha', $senha);

if($stmt->execute()){
    echo "Cadastro realizado com sucesso! <a href='login.php'>Faça login</a>";
} else {
    echo "Erro ao cadastrar.";
}
?>
