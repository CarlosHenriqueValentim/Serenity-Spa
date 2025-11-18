<?php
$host = "localhost";
$port = "3306";
$user = "root";
$senha = "root";
$banco = "Serenity_Spa";

try {
    $conn = new PDO("mysql:host=$host;port=$port;dbname=$banco;charset=utf8", $user, $senha);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch (PDOException $e) {
    echo "ERROR: " . $e->getMessage();
    die();
}
?>
