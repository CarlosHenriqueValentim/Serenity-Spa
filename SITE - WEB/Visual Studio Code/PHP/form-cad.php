<?php
include __DIR__ . '/includes/header.php';
?>
<main>
  <section class="H">
    <div class="I">
      <form action="cad-user.php" method="post">

        <h1>Cadastrar</h1>

        <label>Nome</label>
        <input type="text" name="nome" maxlength="100" placeholder="Nome" required><br>

        <label>RG</label>
        <input type="text" name="rg" placeholder="Exemplo: 00.000.000-0" required maxlength="9"><br>

        <label>Data de Nascimento</label>
        <input type="date" name="data_nasc"><br>

        <label>Email</label>
        <input type="text" name="login" required maxlength="100" placeholder="Endereço de Email" required><br>

        <label>Senha</label>
        <input type="password" name="senha" maxlength="100" placeholder="Senha" required><br>

        <input type="submit" value="Cadastrar">
        <input type="reset" value="Limpar">
    </form>
  </section>
</main>
<?php include __DIR__ . '/includes/footer.php'; ?>
