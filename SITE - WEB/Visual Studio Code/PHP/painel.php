<?php
session_start();
include __DIR__ . '/database.php';
include __DIR__ . '/includes/header.php';

$usuarioLogado = $_SESSION['usuario'] ?? $_SESSION['clientes'] ?? null;

if (!$usuarioLogado) {
    header("Location: index.php");
    exit;
}

$idCliente = $usuarioLogado['id'] ?? $usuarioLogado['codigo_cliente'] ?? null;
$nomeCliente = $usuarioLogado['nome'] ?? $usuarioLogado['nome_cliente'] ?? "Cliente";

if (!$idCliente) {
    header("Location: index.php");
    exit;
}

try {
    $sql = "SELECT a.codigo_agendamento, s.nome_servico, a.data, a.duracao_agendamento, a.status
            FROM agendamentos a
            INNER JOIN servicos s ON s.codigo_servico = a.codigo_servico
            WHERE a.codigo_clientes = :id
            ORDER BY a.data DESC";

    $stmt = $conn->prepare($sql);
    $stmt->bindParam(':id', $idCliente, PDO::PARAM_INT);
    $stmt->execute();
    $agendamentos = $stmt->fetchAll(PDO::FETCH_OBJ);
} catch (PDOException $e) {
    die("Erro ao carregar painel: " . $e->getMessage());
}
?>

<style>
.painel-container {
    max-width: 1200px;
    margin: 40px auto;
    font-family: Arial, sans-serif;
    padding: 20px;
}

.painel-header {
    text-align: center;
    margin-bottom: 40px;
}

.painel-header h1 {
    font-size: 28px;
    margin-bottom: 5px;
}

.painel-header p {
    color: #555;
}

.tabela-agenda {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
    background: #fff;
    border-radius: 8px;
    overflow: hidden;
    box-shadow: 0px 8px 20px rgba(0,0,0,0.1);
}

.tabela-agenda th {
    background: #222;
    color: white;
    padding: 14px;
    text-transform: uppercase;
    font-size: 14px;
}

.tabela-agenda td {
    padding: 14px;
    border-bottom: 1px solid #eee;
    text-align: center;
}

.tabela-agenda tr:nth-child(even) {
    background: #fafafa;
}

.tabela-agenda tr:hover {
    background-color: #f0f8ff;
}

.btn {
    padding: 6px 14px;
    text-decoration: none;
    border-radius: 6px;
    font-size: 13px;
    font-weight: bold;
    transition: 0.3s ease;
    display: inline-block;
}

.btn-editar {
    background: #007bff;
    color: white;
}

.btn-editar:hover {
    background: #0056b3;
}

.btn-excluir {
    background: #dc3545;
    color: white;
}

.btn-excluir:hover {
    background: #a71d2a;
}

.status-confirmado { color: #2e7d32; font-weight: bold; }
.status-pendente { color: #f57c00; font-weight: bold; }
.status-cancelado { color: #c62828; font-weight: bold; }

@media (max-width: 768px) {
    .tabela-agenda th,
    .tabela-agenda td {
        font-size: 13px;
        padding: 10px;
    }
}
</style>

<div class="painel-container">

    <div class="painel-header">
        <h1>Bem-vindo(a), <strong><?= htmlspecialchars($nomeCliente) ?></strong></h1>
        <p>Seus agendamentos realizados no Serenity Spa</p>
    </div>

    <table class="tabela-agenda">
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
        <tbody>
        <?php if (empty($agendamentos)): ?>
            <tr>
                <td colspan="6">Nenhum agendamento encontrado.</td>
            </tr>
        <?php else: ?>
            <?php foreach ($agendamentos as $linha): 
                $status = strtolower($linha->status);
                $classeStatus = "status-" . $status;
            ?>
                <tr>
                    <td><?= htmlspecialchars($linha->nome_servico) ?></td>
                    <td><?= date("d/m/Y H:i", strtotime($linha->data)) ?></td>
                    <td><?= htmlspecialchars($linha->duracao_agendamento) ?> min</td>
                    <td class="<?= $classeStatus ?>"><?= htmlspecialchars($linha->status) ?></td>
                    <td><a href="alterar.php?id=<?= $linha->codigo_agendamento ?>" class="btn btn-editar">Alterar</a></td>
                    <td><a href="excluir.php?id=<?= $linha->codigo_agendamento ?>" class="btn btn-excluir">Cancelar</a></td>
                </tr>
            <?php endforeach; ?>
        <?php endif; ?>
        </tbody>
    </table>

</div>

<?php include __DIR__ . '/includes/footer.php'; ?>
