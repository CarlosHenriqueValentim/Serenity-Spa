<?php
$host = "localhost";
$port = "3306";
$user = "root";
$senha = "root";
$banco = "serenity_spa";

try {
    $conn = new PDO("mysql:host=$host;port=$port;dbname=$banco;charset=utf8", $user, $senha);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch(PDOException $e) {
    echo "Erro: " . $e->getMessage();
    die();
}
?>
