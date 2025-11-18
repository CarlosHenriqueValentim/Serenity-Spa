<?php
// PHP/logout.php
// Garantir sessão
if (session_status() == PHP_SESSION_NONE) session_start();
session_unset();
session_destroy();
header('Location: index.php');
exit;
?>
