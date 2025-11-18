<?php
include __DIR__ . '/database.php';

if (!isset($_POST['nome'], $_POST['desc'], $_POST['data_ini'], $_POST['dia'], $_POST['id'])) {
    header('Location: painel.php');
    exit;
}

try {
    $id = $_POST['id'];
    $nome = $_POST['nome'];
    $desc = $_POST['desc'];
    $data = $_POST['data_ini'];
    $dia = $_POST['dia'];

    $stmt = $conn->prepare("UPDATE agenda SET nome_ag = :nome, desc_ag = :desc, data_ini_ag = :data, dia_ag = :dia WHERE id_ag = :id");
    $stmt->bindParam(':nome', $nome);
    $stmt->bindParam(':desc', $desc);
    $stmt->bindParam(':data', $data);
    $stmt->bindParam(':dia', $dia);
    $stmt->bindParam(':id', $id);
    $stmt->execute();

    header('Location: painel.php');
    exit;
} catch (PDOException $e) {
    echo "ERRO:" . $e->getMessage();
}
?>
