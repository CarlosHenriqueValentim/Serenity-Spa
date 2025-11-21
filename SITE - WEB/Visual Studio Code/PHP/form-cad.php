<?php
include __DIR__ . '/includes/header.php';
session_start();
?>
<main>
  <section class="H">
    <div class="I">
      <form action="cad-cliente.php" method="post">
        <h1>Cadastrar</h1>

        <label>Nome</label>
        <input type="text" name="nome" maxlength="100" placeholder="Digite seu nome completo" required><br>

        <label>Data de Nascimento</label>
        <input type="date" name="data_nasc" required maxlength="10" required><br>

        <label>Telefone</label>
        <input type="text" name="telefone" maxlength="15" placeholder="(11) 91234-5678"><br>

        <label>Email</label>
        <input type="email" name="email" maxlength="100" placeholder="exemplo@gmail.com" required><br>

        <label>Senha</label>
        <input type="password" name="senha" maxlength="100" placeholder="Crie sua Senha" required><br>

        <label>Gênero</label>
        <select name="sexo" required>
            <option value="">Selecione seu gênero</option>
            <option value="m">Masculino</option>
            <option value="f">Feminino</option>
        </select>

        <input type="submit" value="Cadastrar">
        <input type="reset" value="Limpar">
      </form>

      <?php
      if (isset($_GET['erro'])) {
          echo "<p class='msg erro'>" . htmlspecialchars($_GET['erro']) . "</p>";
      }
      ?>
    </div>
  </section>
</main>
<?php include __DIR__ . '/includes/footer.php'; ?>
