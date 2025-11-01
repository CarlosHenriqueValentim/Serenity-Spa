<?php
include __DIR__ . "/includes/header.php";
?>

<main>
  <section class="C">
    <div class="D">
      <form action="cad-agendar.php" method="post">
        <h2>Agende sua Sessão</h2>

        <label>Nome:</label>
        <input type="text" name="nome" placeholder="Digite seu nome" maxlength="100" required><br>

        <label>WhatsApp:</label>
        <input type="tel" name="zap" placeholder="Exemplo: (11) 99999-9999" maxlength="15" required><br>

        <label>Serviço:</label>
        <select name="servico" required>
          <option value="Aromaterapia">Aromaterapia</option>
          <option value="Pedras quentes">Massagem com pedras quentes</option>
          <option value="Shiatsu">Shiatsu</option>
          <option value="Ventosa">Com ventosa</option>
          <option value="Sueca">Sueca</option>
          <option value="Desportiva">Desportiva</option>
          <option value="Esfoliação">Esfoliação completa</option>
          <option value="Envoltório">Envoltório (argilas/chocolates/algas)</option>
          <option value="Limpeza de pele">Limpeza de pele</option>
          <option value="Hidratação facial">Hidratação facial</option>
          <option value="Massagem facial">Massagem facial</option>
          <option value="Sauna">Sauna</option>
          <option value="Pacote">Pacote</option>
          <option value="Banho de sais">Banho com sais</option>
        </select><br>

        <label>Data:</label>
        <input type="date" name="data_agendamento" required><br>

        <label>Horário:</label>
        <input type="time" name="hora" required><br>

        <label>Observações:</label>
        <textarea name="obs" rows="4" placeholder="Digite sua mensagem para entrar em contato com a gente"></textarea><br>

        <input type="submit" value="Enviar Agendamento">
        <input type="reset" value="Limpar">
      </form>
    </div>
  </section>
</main>

<?php
include __DIR__ . "/includes/footer.php";
?>
