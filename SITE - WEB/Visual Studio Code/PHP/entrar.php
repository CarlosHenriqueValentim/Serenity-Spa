<?php
session_start();
include __DIR__ . '/database.php';

if (!isset($_POST['email'], $_POST['senha'])) {
    header('Location: index.php');
    exit;
}

$email = trim($_POST['email']);
$senha = trim($_POST['senha']);

try {
    $sql = "SELECT codigo_cliente, nome_cliente, email_cliente 
            FROM clientes 
            WHERE email_cliente = :email AND senha_cliente = :senha 
            LIMIT 1";

    $consulta = $conn->prepare($sql);
    $consulta->bindParam(':email', $email);
    $consulta->bindParam(':senha', $senha);
    $consulta->execute();

    if ($consulta->rowCount() != 1) {
        header('Location: index.php?erro=' . urlencode('Email ou senha inválidos'));
        exit;
    }

    $data = $consulta->fetch(PDO::FETCH_ASSOC);

    $_SESSION['clientes'] = [
        'codigo' => $data['codigo_cliente'],
        'codigo_cliente' => $data['codigo_cliente'],
        'nome'   => $data['nome_cliente'],
        'nome_cliente' => $data['nome_cliente'],
        'email'  => $data['email_cliente']
    ];

    $_SESSION['usuario'] = [
        'id' => $data['codigo_cliente'],
        'nome' => $data['nome_cliente'],
        'email' => $data['email_cliente']
    ];

    header('Location: painel.php');
    exit;

} catch (PDOException $e) {
    echo "Erro no login: " . htmlspecialchars($e->getMessage());
    exit;
}
