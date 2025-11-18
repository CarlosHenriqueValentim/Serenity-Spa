<?php
include __DIR__ . '/database.php';

if (!isset($_POST['nome'], $_POST['rg'], $_POST['data_nasc'], $_POST['login'], $_POST['senha'])) {
    header('Location: form-cad.php');
    exit;
}

$nome = $_POST['nome'];
$rg = $_POST['rg'];
$data = $_POST['data_nasc'];
$login = $_POST['login'];
$senha = $_POST['senha'];

try {
    $stmt = $conn->prepare("INSERT INTO usuario (nome_user, rg_user, data_nasc_user, data_cad_user, login_user, senha_user) VALUES (:nome, :rg, :data_n, NOW(), :login, :senha);");
    $stmt->bindParam(':nome', $nome);
    $stmt->bindParam(':rg', $rg);
    $stmt->bindParam(':data_n', $data);
    $stmt->bindParam(':login', $login);
    $stmt->bindParam(':senha', $senha);
    $stmt->execute();
    header('Location: index.php');
    exit;
} catch (PDOException $e) {
    echo "Erro: " . $e->getMessage();
}
?>
