<?php
include __DIR__ . '/includes/header-cadastrar.php';
?>
<main>
  <section class="H">
    <div class="I">
      <form action="cad-cliente.php" method="post">
        <h1>Cadastrar</h1>

        <label>Nome</label>
        <input type="text" name="nome" maxlength="100" placeholder="Nome" required><br>

        <label>Data de Nascimento</label>
        <input type="date" name="data_nasc" required><br>

        <label>Email</label>
        <input type="email" name="email" required maxlength="100" placeholder="Endereço de Email"><br>

        <label>Senha</label>
        <input type="password" name="senha" maxlength="100" placeholder="Senha" required><br>

        <input type="submit" value="Cadastrar">
        <input type="reset" value="Limpar">
      </form>
    </div>
  </section>
</main>
<?php
include __DIR__ . '/includes/footer.php';
?>
