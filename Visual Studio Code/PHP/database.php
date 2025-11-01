<?php
//configurando a conexão com o banco
$host="localhost";
$port="3306";
$user="root";
$senha="";
$banco="ss";
try{
    $conn=new PDO('mysql:host='.$host.';port='.$port.';dbname='.$banco,$user,$senha);
    $conn->setAttribute(PDO::ATTR_ERRMODE,PDO::ERRMODE_EXCEPTION);
}catch(PDOException $e){
    echo'ERROR: '.$e->getMessage();
}
?>