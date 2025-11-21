<?php
include __DIR__."/includes/header.php";
session_start();

if (isset($_SESSION['usuario'])) {
    if ($_SESSION['usuario']['tipo'] === "cliente") {
        header("Location: painel.php");
        exit;
    }
    if ($_SESSION['usuario']['tipo'] === "funcionario") {
        header("Location: painel-admin.php");
        exit;
    }
}
?>

<section class="H">
  <div class="I">
    <h1>Entrar</h1>

    <?php if (!empty($_GET['erro'])): ?>
      <p class="msg"><?= htmlspecialchars($_GET['erro']) ?></p>
    <?php endif; ?>

    <form action="entrar.php" method="post">
      <label>Email</label>
      <input type="email" name="email" placeholder="Endereço de Email" required maxlength="100"><br>

      <label>Senha</label>
      <input type="password" name="senha" maxlength="100" placeholder="Senha" required><br>

      <input type="submit" value="Entrar" class="btn">
    </form>

    <p class="msg">Ainda não tem conta? 
      <a href="form-cad.php">Cadastrar</a>
    </p>
  </div>
</section>

<?php include __DIR__ . '/includes/footer.php'; ?>
