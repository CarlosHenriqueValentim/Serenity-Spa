<?php
include __DIR__ . '/database.php';
session_start();

if (!isset($_POST['login'], $_POST['senha'])) {
    header('Location: index.php');
    exit;
}

$login = $_POST['login'];
$senha = $_POST['senha'];

try {
    $consulta = $conn->prepare("SELECT id_user, nome_user, senha_user FROM usuario WHERE login_user = :login AND senha_user = :senha LIMIT 1;");
    $consulta->bindParam(':login', $login);
    $consulta->bindParam(':senha', $senha);
    $consulta->execute();
    $quant = $consulta->rowCount();

    if ($quant != 1) {
        header('Location: index.php?erro=' . urlencode('Login ou senha inválidos.'));
        exit;
    }

    $data = $consulta->fetch(PDO::FETCH_OBJ);

    $_SESSION['usuario'] = ['id' => $data->id_user, 'nome' => $data->nome_user];
    header('Location: painel.php');
    exit;
} catch (PDOException $e) {
    echo "Erro: " . $e->getMessage();
}
?>
