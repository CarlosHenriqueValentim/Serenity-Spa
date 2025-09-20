<?php
session_start();


$nome = trim($_POST['nome'] ?? '');
$email = trim($_POST['email'] ?? '');
$senha = trim($_POST['senha'] ?? '');
$confirma = trim($_POST['confirmar_senha'] ?? '');


if (empty($nome) || empty($email) || empty($senha)) {
    header("Location: index.php?erro=3");
    exit;
}


$arquivo = __DIR__ . "/usuarios.csv";


$usuarios = file_exists($arquivo) ? array_map('str_getcsv', file($arquivo)) : [];

$usuario_encontrado = false;


foreach ($usuarios as $user) {
    if ($user[1] === $email) { 
        $usuario_encontrado = true;
        if (password_verify($senha, $user[2])) {
           
            $_SESSION['nome'] = $user[0];
            $_SESSION['email'] = $user[1];
            header("Location: agendar.php");
            exit;
        } else {
            
            header("Location: index.php?erro=1");
            exit;
        }
    }
}


if (!$usuario_encontrado) {
    if ($senha !== $confirma) {
        header("Location: index.php?erro=2");
        exit;
    }

    $senha_hash = password_hash($senha, PASSWORD_DEFAULT);
    $linha = [$nome, $email, $senha_hash];

   
    $arquivo_csv = fopen($arquivo, 'a');
    if ($arquivo_csv === false) {
        die("❌ Erro ao abrir o arquivo de usuários.");
    }

    fputcsv($arquivo_csv, $linha); 
    fclose($arquivo_csv);

    $_SESSION['nome'] = $nome;
    $_SESSION['email'] = $email;

    header("Location: agendar.php");
    exit;
}
