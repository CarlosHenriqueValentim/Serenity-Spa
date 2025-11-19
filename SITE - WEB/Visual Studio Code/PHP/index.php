<?php
include __DIR__ . '/includes/header.php';
session_start();

if (!empty($_SESSION['clientes'])) {
    header('Location: painel.php');
    exit;
}
?>
<section class="H">
  <div class="I">
    <h1>Entrar</h1>
    <?php if (!empty($_GET['erro'])) echo '<p class="msg">'.htmlspecialchars($_GET['erro']).'</p>'; ?>
    
    <form action="entrar.php" method="post">
      <label>Email</label>
      <input type="email" name="email" required maxlength="100"><br>

      <label>Senha</label>
      <input type="password" name="senha" required maxlength="100"><br>

      <input type="submit" value="Entrar" class="btn">
    </form>

    <p class="msg">Ainda não tem conta? <a href="form-cad.php">Cadastrar</a></p>
  </div>
</section>

<?php include __DIR__ . '/includes/footer.php'; ?>
