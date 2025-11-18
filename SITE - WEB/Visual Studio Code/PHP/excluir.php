<?php
include __DIR__ . '/database.php';

if (!isset($_GET['id'])) {
    header('Location: painel.php');
    exit;
}
$id = $_GET['id'];

try {
    $stmt = $conn->prepare("DELETE FROM agenda WHERE id_ag = :id");
    $stmt->bindParam(':id', $id);
    $stmt->execute();
    header('Location: painel.php');
    exit;
} catch (PDOException $e) {
    echo "ERRO:" . $e->getMessage();
}
?>
