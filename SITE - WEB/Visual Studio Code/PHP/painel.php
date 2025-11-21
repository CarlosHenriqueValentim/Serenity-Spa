<?php
include __DIR__.'/includes/header.php'; 
include __DIR__.'/database.php';
session_start();

$usuarioLogado = $_SESSION['usuario'] ?? $_SESSION['clientes'] ?? null;

if (!$usuarioLogado) {
    header('Location: index.php');
    exit;
}

$idCliente = $usuarioLogado['id'] ?? $usuarioLogado['codigo_cliente'] ?? null;
$nomeCliente = $usuarioLogado['nome'] ?? $usuarioLogado['nome_cliente'] ?? 'Cliente';

if (!$idCliente) {
    header('Location: index.php');
    exit;
}
?>

<section class="B">
<h1 class="painel-nome">Bem-vindo(a) <strong><?= htmlspecialchars($nomeCliente) ?></strong></h1>
<p>Seus Agendamentos</p>
</section>

<?php
try {
    $sql = "SELECT a.codigo_agendamento, s.nome_servico, a.data, a.duracao_agendamento, a.status
            FROM agendamentos AS a
            INNER JOIN servicos AS s ON s.codigo_servico = a.codigo_servico
            WHERE a.codigo_clientes = :id
            ORDER BY a.data DESC";
    $stmt = $conn->prepare($sql);
    $stmt->bindParam(':id', $idCliente, PDO::PARAM_INT);
    $stmt->execute();
    $respostas = $stmt->fetchAll(PDO::FETCH_OBJ);
} catch (PDOException $e) {
    echo "<p style='color:red'>Erro na consulta: " . htmlspecialchars($e->getMessage()) . "</p>";
    exit;
}

echo "<table class='tabela-agenda'>
        <thead>
            <tr>
                <th>Serviço</th>
                <th>Data</th>
                <th>Duração</th>
                <th>Status</th>
                <th>Alterar</th>
                <th>Cancelar</th>
            </tr>
        </thead>
        <tbody>";

if (empty($respostas)) {
    echo "<tr><td colspan='6'>PRESSIONE AGENDAR PARA COMEÇAR</td></tr>";
} else {
    foreach ($respostas as $linha) {
        echo "<tr>
                <td>" . htmlspecialchars($linha->nome_servico) . "</td>
                <td>" . htmlspecialchars($linha->data) . "</td>
                <td>" . htmlspecialchars($linha->duracao_agendamento) . "</td>
                <td>" . htmlspecialchars($linha->status) . "</td>
                <td><a href='alterar.php?id=" . urlencode($linha->codigo_agendamento) . "'>Alterar</a></td>
                <td><a href='excluir.php?id=" . urlencode($linha->codigo_agendamento) . "'>Cancelar</a></td>
              </tr>";
    }
}

echo "</tbody></table>";
?>

<?php include __DIR__.'/includes/footer.php'; ?>
