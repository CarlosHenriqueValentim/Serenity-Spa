<?php
include __DIR__ . '/database.php';
session_start(); 

if (!isset($_POST['email'], $_POST['senha'])) {
    header("location: index.php?erro=preencha todos os campos");
    exit;
}

$email = strtolower(trim($_POST['email']));
$senha = trim($_POST['senha']);

try {
    $stmt = $conn->prepare("select * from clientes where lower(trim(email_cliente)) = :email limit 1");
    $stmt->bindParam(':email', $email);
    $stmt->execute();
    $usuario = $stmt->fetch(PDO::FETCH_ASSOC);

    if ($usuario && trim($usuario['senha_cliente']) === $senha) {
        $_SESSION['usuario'] = [
            'id' => $usuario['codigo_cliente'],
            'nome' => $usuario['nome_cliente'],
            'tipo' => 'cliente'
        ];
        header("location: painel.php");
        exit;
    }

    $stmt = $conn->prepare("select * from funcionarios where lower(trim(email_funcionario)) = :email limit 1");
    $stmt->bindParam(':email', $email);
    $stmt->execute();
    $usuario = $stmt->fetch(PDO::FETCH_ASSOC);

    if ($usuario && trim($usuario['senha_funcionario']) === $senha) {
        $_SESSION['usuario'] = [
            'id' => $usuario['codigo_funcionario'],
            'nome' => $usuario['nome_funcionario'],
            'tipo' => 'funcionario',
            'cargo' => $usuario['cargo_funcionario']
        ];
        header("location: painel-admin.php");
        exit;
    }

    header("location: index.php?erro=email ou senha incorretos");
    exit;

} catch (PDOException $e) {
    echo "erro: " . $e->getMessage();
}
