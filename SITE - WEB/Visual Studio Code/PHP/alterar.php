<?php
include __DIR__ . '/database.php';
include __DIR__ . '/includes/header.php';

if (!isset($_GET['id'])) {
    header('Location: painel.php');
    exit;
}

$id = $_GET['id'];

try {
    $stmt = $conn->prepare("SELECT * FROM agenda WHERE id_ag = :id LIMIT 1;");
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
  <label>Nome:</label>
  <input type="text" name="nome" value="<?php echo htmlspecialchars($dados->nome_ag); ?>"><br>
  <label>Descrição:</label>
  <textarea name="desc"><?php echo htmlspecialchars($dados->desc_ag); ?></textarea><br>
  <label>Data:</label>
  <input type="date" name="data_ini" value="<?php echo htmlspecialchars($dados->data_ini_ag); ?>"><br>
  <label>Dias:</label>
  <input type="number" name="dia" value="<?php echo htmlspecialchars($dados->dia_ag); ?>"><br>
  <input type="hidden" name="id" value="<?php echo htmlspecialchars($dados->id_ag); ?>">
  <input type="submit" value="Alterar">
</form>

<?php include __DIR__ . '/includes/footer.php'; ?>
