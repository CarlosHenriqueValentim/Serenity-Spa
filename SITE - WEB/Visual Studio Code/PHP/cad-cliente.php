<?php
include __DIR__ . '/database.php';

if (!isset($_POST['nome'], $_POST['data_nasc'], $_POST['email'], $_POST['senha'])) {
    header('Location: form-cad.php');
    die();
}

$nome = trim($_POST['nome']);
$data = $_POST['data_nasc'];
$email = trim($_POST['email']);
$senha = trim($_POST['senha']);

try {
    $stmt = $conn->prepare("
        INSERT INTO clientes
        (codigo_empresa, nome_cliente, data_nasc_cliente, telefone_cliente, email_cliente, senha_cliente, sexo_cliente)
        VALUES
        (:codigo_empresa, :nome, :data_nasc, :telefone, :email, :senha, :sexo)
    ");

    $codigo_empresa = 1;
    $telefone = isset($_POST['telefone']) ? trim($_POST['telefone']) : null;
    $sexo = isset($_POST['sexo']) ? trim($_POST['sexo']) : 'm';

    $stmt->bindParam(':codigo_empresa', $codigo_empresa, PDO::PARAM_INT);
    $stmt->bindParam(':nome', $nome);
    $stmt->bindParam(':data_nasc', $data);
    $stmt->bindParam(':telefone', $telefone);
    $stmt->bindParam(':email', $email);
    $stmt->bindParam(':senha', $senha);
    $stmt->bindParam(':sexo', $sexo);

    $stmt->execute();

    header('Location: index.php');
    die();
} catch (PDOException $e) {
    echo "Erro: " . $e->getMessage();
}
?>
