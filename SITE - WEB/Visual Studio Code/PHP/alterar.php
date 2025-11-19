<?php
include __DIR__ . '/database.php';
include __DIR__ . '/includes/header.php';

if (!isset($_GET['id'])) {
    header('Location: painel.php');
    exit;
}

$id = $_GET['id'];

try {
    $stmt = $conn->prepare("
        SELECT a.*, s.nome_servico
        FROM agendamentos a
        INNER JOIN servicos s ON s.codigo_servico = a.codigo_servico
        WHERE codigo_agendamento = :id
        LIMIT 1
    ");
    $stmt->bindParam(':id', $id);
    $stmt->execute();
    $dados = $stmt->fetch(PDO::FETCH_OBJ);

    if (!$dados) {
        echo "<p>Registro não encontrado.</p>";
        include __DIR__ . '/includes/footer.php';
        exit;
    }
} catch (PDOException $e) {
    echo "ERRO: " . $e->getMessage();
    include __DIR__ . '/includes/footer.php';
    exit;
}
?>

<form action="update.php" method="post">
    <label>Serviço:</label>
    <input type="text" value="<?= htmlspecialchars($dados->nome_servico) ?>" disabled><br>

    <label>Data:</label>
    <input type="date" name="data" value="<?= htmlspecialchars($dados->data) ?>"><br>

    <label>Duração:</label>
    <input type="time" name="duracao_agendamento" value="<?= htmlspecialchars($dados->duracao_agendamento) ?>"><br>

    <label>Status:</label>
    <select name="status">
        <option value="agendado"   <?= $dados->status == "agendado" ? "selected" : "" ?>>Agendado</option>
        <option value="concluido"  <?= $dados->status == "concluido" ? "selected" : "" ?>>Concluído</option>
        <option value="cancelado"  <?= $dados->status == "cancelado" ? "selected" : "" ?>>Cancelado</option>
    </select><br>

    <input type="hidden" name="id" value="<?= htmlspecialchars($dados->codigo_agendamento) ?>">

    <input type="submit" value="Alterar">
</form>

<?php include __DIR__ . '/includes/footer.php'; ?>
