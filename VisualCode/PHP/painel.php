<?php
session_start();
if(!isset($_SESSION['usuario'])){
    header("Location: login.php");
    die();
}
include __DIR__."/includes/header.php";
?>

<h2>Painel do Cliente</h2>
<p>Bem-vindo, <?php echo $_SESSION['usuario']; ?>!</p>
<p>Aqui você pode visualizar seus serviços e agendamentos (futuro CRUD).</p>

<?php include __DIR__."/includes/footer.php"; ?>
