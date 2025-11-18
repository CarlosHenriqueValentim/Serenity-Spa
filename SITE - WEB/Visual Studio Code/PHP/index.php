<?php
include __DIR__ . '/database.php';
include __DIR__ . '/includes/header.php';

// Se já estiver logado, redireciona
if (!empty($_SESSION['usuario'])) {
    header('Location: painel.php');
    exit;
}

// Formulário de login simples — o processamento fica em logar.php
?>
<section class="H">
  <div class="I">
    <h1>Entrar</h1>
    <?php if (!empty($_GET['erro'])) echo '<p class="msg">'.htmlspecialchars($_GET['erro']).'</p>'; ?>
    <form action="logar.php" method="post">
      <label>Email</label>
      <input type="text" name="login" maxlength="100" placeholder="Endereço de Email"  required><br>

      <label>Senha</label>
      <input type="password" name="senha" maxlength="100" placeholder="Senha" required><br>

      <input type="submit" value="Entrar" class="btn">
    </form>
    <p class="msg">Ainda não tem conta? <a href="form-cad.php">Cadastrar</a></p>
  </div>
</section>
<?php include __DIR__ . '/includes/footer.php'; ?>
