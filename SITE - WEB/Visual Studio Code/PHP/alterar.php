<?php
include __DIR__ . '/database.php';
include __DIR__ . '/includes/header.php';

if (!isset($_GET['id']) || empty($_GET['id'])) {
    header('Location: painel.php');
    exit;
}

$id = $_GET['id'];

try {

    $stmt = $conn->prepare("
        SELECT a.*, s.codigo_servico, s.nome_servico
        FROM agendamentos a
        INNER JOIN servicos s 
        ON s.codigo_servico = a.codigo_servico
        WHERE a.codigo_agendamento = :id
        LIMIT 1
    ");

    $stmt->bindParam(':id', $id, PDO::PARAM_INT);
    $stmt->execute();
    $dados = $stmt->fetch(PDO::FETCH_OBJ);

    if (!$dados) {
        echo "<p class='msg'>Agendamento não encontrado.</p>";
        include __DIR__ . '/includes/footer.php';
        exit;
    }

    $stmtServicos = $conn->prepare("SELECT codigo_servico, nome_servico FROM servicos ORDER BY nome_servico");
    $stmtServicos->execute();
    $servicos = $stmtServicos->fetchAll(PDO::FETCH_ASSOC);

} catch (PDOException $e) {
    echo "<p class='msg'>Erro ao carregar agendamento: {$e->getMessage()}</p>";
    include __DIR__ . '/includes/footer.php';
    exit;
}
?>

<main class="CorpoPadrão">
<section class="H">
  <div class="I">

    <h1>Editar Agendamento</h1>

    <form action="update.php" method="post">

      <label>Serviço</label>
      <select name="servico" required>
          <?php foreach ($servicos as $servico): ?>
              <option value="<?= $servico['codigo_servico'] ?>"
                <?= ($servico['codigo_servico'] == $dados->codigo_servico) ? 'selected' : '' ?>>
                  <?= htmlspecialchars($servico['nome_servico']) ?>
              </option>
          <?php endforeach; ?>
      </select>

      <label>Data</label>
      <input type="date" name="data" value="<?= htmlspecialchars($dados->data) ?>" required>

      <label>Duração</label>
      <input type="time" name="duracao_agendamento" 
             value="<?= htmlspecialchars($dados->duracao_agendamento) ?>" required>

      <label>Status</label>
      <select name="status" required>
        <option value="agendado" <?= $dados->status == "agendado" ? "selected" : "" ?>>Agendado</option>
        <option value="concluido" <?= $dados->status == "concluido" ? "selected" : "" ?>>Concluído</option>
        <option value="cancelado" <?= $dados->status == "cancelado" ? "selected" : "" ?>>Cancelado</option>
      </select>

      <input type="hidden" name="id" value="<?= $dados->codigo_agendamento ?>">

      <input type="submit" value="Alterar">

    </form>

  </div>
</section>
</main>

<?php include __DIR__ . '/includes/footer.php'; ?>
