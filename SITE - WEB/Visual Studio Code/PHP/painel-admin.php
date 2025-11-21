<?php
session_start();
require __DIR__ . '/database.php';

if (!isset($_SESSION['usuario']) || $_SESSION['usuario']['tipo'] !== 'funcionario') {
    header('Location: index.php');
    exit;
}

$nome  = $_SESSION['usuario']['nome'];
$cargo = $_SESSION['usuario']['cargo'];

include __DIR__ . '/includes/header.php';

try {
    $totalAgendas     = $conn->query("SELECT COUNT(*) FROM agendamentos")->fetchColumn();
    $totalClientes    = $conn->query("SELECT COUNT(*) FROM clientes")->fetchColumn();
    $totalServicos    = $conn->query("SELECT COUNT(*) FROM servicos")->fetchColumn();
    $totalPacotes     = $conn->query("SELECT COUNT(*) FROM pacotes")->fetchColumn();
    $totalProdutos    = $conn->query("SELECT COUNT(*) FROM produtos_estoque")->fetchColumn();
    $totalReceita     = $conn->query("SELECT IFNULL(SUM(valor_financeiro),0) FROM financeiro WHERE tipo_financeiro='receita'")->fetchColumn();
    $totalDespesa     = $conn->query("SELECT IFNULL(SUM(valor_financeiro),0) FROM financeiro WHERE tipo_financeiro='despesa'")->fetchColumn();

    $stmtAgendas = $conn->query("
        SELECT 
            a.codigo_agendamento,
            c.nome_cliente,
            c.email_cliente,
            f.nome_funcionario,
            s.nome_servico,
            a.data,
            a.duracao_agendamento,
            a.status
        FROM agendamentos a
        JOIN clientes c ON a.codigo_clientes = c.codigo_cliente
        JOIN funcionarios f ON a.codigo_funcionario = f.codigo_funcionario
        JOIN servicos s ON a.codigo_servico = s.codigo_servico
        ORDER BY a.data DESC
        LIMIT 20
    ");

 
    $stmtClientes = $conn->query("SELECT * FROM clientes ORDER BY nome_cliente ASC");


    $stmtServicos = $conn->query("SELECT * FROM servicos ORDER BY nome_servico ASC");


    $stmtPacotes = $conn->query("SELECT * FROM pacotes ORDER BY nome_pacote ASC");


    $stmtProdutos = $conn->query("SELECT * FROM produtos_estoque ORDER BY nome_produto_estoque ASC");


    $stmtFinanceiro = $conn->query("SELECT * FROM financeiro ORDER BY data_financeiro DESC");

} catch (PDOException $e) {
    echo "Erro ao buscar dados: " . $e->getMessage();
    exit;
}
?>

<div class="painel-container">
    <div class="painel-topo">
        <h2>Painel Administrativo</h2>
        <p>Bem-vindo(a): <strong><?= htmlspecialchars($nome) ?></strong> | Cargo: <?= htmlspecialchars($cargo) ?></p>
    </div>
    <section class="painel-resumo">
        <div class="card-resumo azul">
            <h3>Agendamentos</h3>
            <span><?= $totalAgendas ?></span>
        </div>
        <div class="card-resumo verde">
            <h3>Clientes</h3>
            <span><?= $totalClientes ?></span>
        </div>
        <div class="card-resumo roxo">
            <h3>Serviços</h3>
            <span><?= $totalServicos ?></span>
        </div>
        <div class="card-resumo laranja">
            <h3>Pacotes</h3>
            <span><?= $totalPacotes ?></span>
        </div>
        <div class="card-resumo rosa">
            <h3>Produtos</h3>
            <span><?= $totalProdutos ?></span>
        </div>
        <div class="card-resumo cinza">
            <h3>Financeiro</h3>
            <span>R$ <?= number_format($totalReceita - $totalDespesa, 2, ",", ".") ?></span>
        </div>
    </section>
    <section class="painel-menu">
        <h2>Menu de Acesso</h2>
        <ul>
            <li><a href="#agendamentos">Agendamentos</a></li>
            <li><a href="#clientes">Clientes</a></li>
            <li><a href="#servicos">Serviços</a></li>
            <li><a href="#pacotes">Pacotes</a></li>
            <li><a href="#produtos">Produtos</a></li>
            <li><a href="#financeiro">Financeiro</a></li>
        </ul>
    </section>
    <section id="agendamentos" class="painel-tabela">
        <h2>Agendamentos Recentes</h2>
        <div class="table-container">
            <table class="tabela-agenda">
                <thead>
                    <tr>
                        <th>Cliente</th>
                        <th>Email</th>
                        <th>Funcionario</th>
                        <th>Serviço</th>
                        <th>Data</th>
                        <th>Duração</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    <?php while ($row = $stmtAgendas->fetch(PDO::FETCH_ASSOC)): ?>
                                        <tr>
                                        <td><?= htmlspecialchars($row['nome_cliente']) ?></td>
                                        <td><?= htmlspecialchars($row['email_cliente']) ?></td>
                                        <td><?= htmlspecialchars($row['nome_funcionario']) ?></td>
                                        <td><?= htmlspecialchars($row['nome_servico']) ?></td>
                                        <td><?= date("d/m/Y", strtotime($row['data'])) ?></td>
                                        <td><?= htmlspecialchars($row['duracao_agendamento']) ?></td>
                                        <td><?= htmlspecialchars($row['status']) ?></td>
                                    <td>
                                <a href="mailto:<?= htmlspecialchars($row['email_cliente']) ?>?subject=Sobre seu agendamento&body=Olá <?= htmlspecialchars($row['nome_cliente']) ?>,%0AVamos confirmar seu agendamento para o serviço <?= htmlspecialchars($row['nome_servico']) ?> na data <?= date('d/m/Y', strtotime($row['data'])) ?>.%0A%0AAtenciosamente,%0ASua equipe Serenity Spa" class="btn-email">Enviar Mensagem</a>
                            </td>
                        </tr>
                    <?php endwhile; ?>
                </tbody>
            </table>
        </div>
    </section>
    <section id="clientes" class="painel-tabela">
        <h2>Clientes</h2>
        <div class="table-container">
            <table class="tabela-cliente">
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Email</th>
                        <th>Telefone</th>
                        <th>Data de Nascimento</th>
                        <th>Sexo</th>
                    </tr>
                </thead>
                <tbody>
                    <?php while ($row = $stmtClientes->fetch(PDO::FETCH_ASSOC)): ?>
                    <tr>
                        <td><?= htmlspecialchars($row['nome_cliente']) ?></td>
                        <td><?= htmlspecialchars($row['email_cliente']) ?></td>
                        <td><?= htmlspecialchars($row['telefone_cliente']) ?></td>
                        <td><?= date("d/m/Y", strtotime($row['data_nasc_cliente'])) ?></td>
                        <td><?= htmlspecialchars($row['sexo_cliente']) ?></td>
                    </tr>
                    <?php endwhile; ?>
                </tbody>
            </table>
        </div>
    </section>
    <section id="servicos" class="painel-tabela">
        <h2>Serviços</h2>
        <div class="table-container">
            <table class="tabela-servico">
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Descrição</th>
                        <th>Duração Min-Max</th>
                        <th>Preço Min-Max</th>
                    </tr>
                </thead>
                <tbody>
                    <?php while ($row = $stmtServicos->fetch(PDO::FETCH_ASSOC)): ?>
                    <tr>
                        <td><?= htmlspecialchars($row['nome_servico']) ?></td>
                        <td><?= htmlspecialchars($row['descricao_servico']) ?></td>
                        <td><?= $row['duracao_min_servico'] ?> - <?= $row['duracao_max_servico'] ?> min</td>
                        <td>R$ <?= number_format($row['preco_min_servico'], 2, ",", ".") ?> - R$ <?= number_format($row['preco_max_servico'], 2, ",", ".") ?></td>
                    </tr>
                    <?php endwhile; ?>
                </tbody>
            </table>
        </div>
    </section>
    <section id="pacotes" class="painel-tabela">
        <h2>Pacotes</h2>
        <div class="table-container">
            <table class="tabela-pacote">
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Descrição</th>
                        <th>Duração</th>
                        <th>Preço</th>
                        <th>Valor Extra</th>
                    </tr>
                </thead>
                <tbody>
                    <?php while ($row = $stmtPacotes->fetch(PDO::FETCH_ASSOC)): ?>
                    <tr>
                        <td><?= htmlspecialchars($row['nome_pacote']) ?></td>
                        <td><?= htmlspecialchars($row['descricao_pacote']) ?></td>
                        <td><?= $row['duracao_pacote'] ?> min</td>
                        <td>R$ <?= number_format($row['preco_pacote'], 2, ",", ".") ?></td>
                        <td>R$ <?= number_format($row['valor_por_pessoa_extra_pacote'], 2, ",", ".") ?></td>
                    </tr>
                    <?php endwhile; ?>
                </tbody>
            </table>
        </div>
    </section>
    <section id="produtos" class="painel-tabela">
        <h2>Produtos em Estoque</h2>
        <div class="table-container">
            <table class="tabela-produto">
                <thead>
                    <tr>
                        <th>Produto</th>
                        <th>Categoria</th>
                        <th>Quantidade</th>
                        <th>Preço Unitário</th>
                        <th>Preço Total</th>
                        <th>Fornecedor</th>
                    </tr>
                </thead>
                <tbody>
                    <?php while ($row = $stmtProdutos->fetch(PDO::FETCH_ASSOC)): ?>
                    <tr>
                        <td><?= htmlspecialchars($row['nome_produto_estoque']) ?></td>
                        <td><?= htmlspecialchars($row['categoria_produto_estoque']) ?></td>
                        <td><?= $row['quantidade_produto_estoque'] ?></td>
                        <td>R$ <?= number_format($row['preco_unitario_produto_estoque'], 2, ",", ".") ?></td>
                        <td>R$ <?= number_format($row['preco_total_produto_estoque'], 2, ",", ".") ?></td>
                        <td><?= htmlspecialchars($row['fornecedor_produto_estoque']) ?></td>
                    </tr>
                    <?php endwhile; ?>
                </tbody>
            </table>
        </div>
    </section>
    <section id="financeiro" class="painel-tabela">
        <h2>Financeiro</h2>
        <div class="table-container">
            <table class="tabela-financeiro">
                <thead>
                    <tr>
                        <th>Tipo</th>
                        <th>Descrição</th>
                        <th>Valor</th>
                        <th>Data</th>
                    </tr>
                </thead>
                <tbody>
                    <?php while ($row = $stmtFinanceiro->fetch(PDO::FETCH_ASSOC)): ?>
                    <tr>
                        <td><?= htmlspecialchars($row['tipo_financeiro']) ?></td>
                        <td><?= htmlspecialchars($row['descricao_financeiro']) ?></td>
                        <td>R$ <?= number_format($row['valor_financeiro'], 2, ",", ".") ?></td>
                        <td><?= date("d/m/Y", strtotime($row['data_financeiro'])) ?></td>
                    </tr>
                    <?php endwhile; ?>
                </tbody>
            </table>
        </div>
    </section>
</div>

<?php include __DIR__ . '/includes/footer.php'; ?>
