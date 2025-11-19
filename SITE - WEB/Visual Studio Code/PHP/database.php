<?php
$host = "localhost";
$porta = "3306";
$usuario = "root";
$senha = "root";
$banco = "Serenity_Spa";

try {
    $conn = new PDO("mysql:host=$host;port=$porta;dbname=$banco", $usuario, $senha);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch (PDOException $e) {
    echo "ERROR: " . $e->getMessage();
    die();
}
?>
