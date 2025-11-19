<?php
include __DIR__.'/includes/header.php';
include __DIR__.'/database.php';
session_start();

$idCliente = null;
$nomeCliente = null;

function buscarClienteSessao($sessao) {
    $id = $nome = null;
    if (!empty($sessao)) {
        if (!empty($sessao['id'])) $id = $sessao['id'];
        if (!empty($sessao['codigo_cliente'])) $id = $sessao['codigo_cliente'];
        if (!empty($sessao['codigo'])) $id = $sessao['codigo'];
        if (!empty($sessao['nome'])) $nome = $sessao['nome'];
        if (!empty($sessao['nome_cliente'])) $nome = $sessao['nome_cliente'];
    }
    return [$id, $nome];
}


list($idCliente, $nomeCliente) = buscarClienteSessao($_SESSION['clientes'] ?? []);
if (!$idCliente) list($idCliente, $nomeCliente) = buscarClienteSessao($_SESSION['cliente'] ?? []);
if (!$idCliente) list($idCliente, $nomeCliente) = buscarClienteSessao($_SESSION['usuario'] ?? []);


if (!$idCliente) {
    header('Location: index.php');
    exit;
}

if (!$nomeCliente) {
    try {
        $q = $conn->prepare("SELECT nome_cliente FROM clientes WHERE codigo_cliente = :id LIMIT 1");
        $q->bindParam(':id', $idCliente, PDO::PARAM_INT);
        $q->execute();
        $r = $q->fetch(PDO::FETCH_ASSOC);
        if ($r && !empty($r['nome_cliente'])) $nomeCliente = $r['nome_cliente'];
    } catch (PDOException $e) {
        $nomeCliente = null;
    }
}
?>

    <h1 class="painel-nome"><?= htmlspecialchars($nomeCliente ?? 'Cliente') ?></h1>

<main>
    <section class="table-container">
        <?php

        try {
            $check = $conn->query("SHOW TABLES LIKE 'agendamentos'")->fetchAll();
            if (count($check) === 0) {
                echo "<p style='color:red'>Erro: tabela <strong>agendamentos</strong> não encontrada.</p>";
                exit;
            }
        } catch (PDOException $e) {
            echo "<p style='color:red'>Erro ao verificar tabela: " . htmlspecialchars($e->getMessage()) . "</p>";
            exit;
        }

        try {
            $sql = "
                SELECT 
                    a.codigo_agendamento,
                    s.nome_servico,
                    a.data,
                    a.duracao_agendamento,
                    a.status
                FROM agendamentos AS a
                INNER JOIN servicos AS s ON s.codigo_servico = a.codigo_servico
                WHERE a.codigo_clientes = :id
                ORDER BY a.data DESC
            ";
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
            echo "<tr><td colspan='6'>Nenhum agendamento encontrado.</td></tr>";
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
    </section>
</main>

<?php
include __DIR__.'/includes/footer.php';
?>
