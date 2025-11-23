<?php
session_start();

require __DIR__ . '/database.php';


if (!isset($_SESSION['usuario']) || $_SESSION['usuario']['tipo'] !== 'funcionario') {
    header('Location: index.php');
    exit;
}

$nome  = $_SESSION['usuario']['nome'];
$cargo = $_SESSION['usuario']['cargo'];


if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['mensagem'], $_POST['agendamento_id'])) {

    $msg = trim($_POST['mensagem']);
    $agendamentoId = $_POST['agendamento_id'];
    $idFuncionario = $_SESSION['usuario']['id'] ?? $_SESSION['usuario']['codigo_funcionario']; 

    if (!empty($msg)) {
        try {
        
            $stmt = $conn->prepare("
                INSERT INTO mensagens 
                (codigo_agendamento, remetente_tipo, remetente_id, mensagem)
                VALUES (:ag, 'funcionario', :id, :msg)
            ");
    
            $stmt->execute([
                ':ag' => $agendamentoId,
                ':id' => $idFuncionario,
                ':msg' => $msg
            ]);
        } catch (PDOException $e) {
           
        }
    }

    header("Location: painel-admin.php");
    exit;
}

try {
    $totalAgendas     = $conn->query("SELECT COUNT(*) FROM agendamentos")->fetchColumn();
    $totalClientes    = $conn->query("SELECT COUNT(*) FROM clientes")->fetchColumn();
    $totalServicos    = $conn->query("SELECT COUNT(*) FROM servicos")->fetchColumn();
    $totalPacotes     = $conn->query("SELECT COUNT(*) FROM pacotes")->fetchColumn();
    $totalProdutos    = $conn->query("SELECT COUNT(*) FROM produtos_estoque")->fetchColumn();
    $totalReceita     = $conn->query("SELECT IFNULL(SUM(valor_financeiro),0) FROM financeiro WHERE tipo_financeiro='receita'")->fetchColumn();
    $totalDespesa     = $conn->query("SELECT IFNULL(SUM(valor_financeiro),0) FROM financeiro WHERE tipo_financeiro='despesa'")->fetchColumn();
    $saldoFinanceiro  = $totalReceita - $totalDespesa;

    $stmtAgendas = $conn->query("
        SELECT 
            a.codigo_agendamento, c.nome_cliente, c.email_cliente, f.nome_funcionario, s.nome_servico,
            a.data, a.duracao_agendamento, a.status
        FROM agendamentos a
        JOIN clientes c ON a.codigo_clientes = c.codigo_cliente
        JOIN funcionarios f ON a.codigo_funcionario = f.codigo_funcionario
        JOIN servicos s ON a.codigo_servico = s.codigo_servico
        ORDER BY a.data DESC
        LIMIT 20
    ");
 
    $stmtClientes = $conn->query("SELECT nome_cliente, email_cliente, telefone_cliente, data_nasc_cliente, sexo_cliente FROM clientes ORDER BY nome_cliente ASC");
    $stmtServicos = $conn->query("SELECT nome_servico, descricao_servico, duracao_min_servico, duracao_max_servico, preco_min_servico, preco_max_servico FROM servicos ORDER BY nome_servico ASC");
    $stmtPacotes = $conn->query("SELECT nome_pacote, descricao_pacote, duracao_pacote, preco_pacote, valor_por_pessoa_extra_pacote FROM pacotes ORDER BY nome_pacote ASC");
    $stmtProdutos = $conn->query("SELECT nome_produto_estoque, categoria_produto_estoque, quantidade_produto_estoque, preco_unitario_produto_estoque, preco_total_produto_estoque, fornecedor_produto_estoque FROM produtos_estoque ORDER BY nome_produto_estoque ASC");
    $stmtFinanceiro = $conn->query("SELECT tipo_financeiro, descricao_financeiro, valor_financeiro, data_financeiro FROM financeiro ORDER BY data_financeiro DESC");

} catch (PDOException $e) {
    echo "Erro ao buscar dados: " . $e->getMessage();
    exit;
}

?>
<!DOCTYPE html>
<html lang="pt-br">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Painel Serenity Spa - Administrativo</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    
    <style>
        @font-face { font-family: power; src: url(../FONTES/NewYork\ PERSONAL\ USE.otf); }
        @font-face { font-family: gelo; src: url(../FONTES/apple_garamond_Fontes_para_Texto_simples/AppleGaramond-Light.ttf); }
        @font-face { font-family: fogo; src: url(../FONTES/arial_narrow_7.ttf); }
        
        * { margin: 0; padding: 0; box-sizing: border-box; }
        
        body {
            font-family: "Segoe UI", sans-serif;
            line-height: 1.6;
            background: linear-gradient(135deg, #fff0f5, #fddde6);
            color: #5a3d46;
            position: relative;
            overflow-x: hidden;
            margin: 0;
            padding-bottom: 60px; 
        }

        .power { font-family: 'power', serif; }
        .gelo { font-family: 'gelo', serif; }
        .fogo { font-family: 'fogo', serif; }

        .cor-primaria { color: #9b3f5f; } 
        .cor-secundaria { color: #ff7797; } 
        .cor-fundo-cartao { background-color: rgba(255, 255, 255, 0.94); }
        .borda-cartao { border: 1px solid #ffd1df; }
        .gradiente-header { background: linear-gradient(90deg, #f8b4c4, #e07a9a); } /* Header, Tabelas */

        .header-principal {
            display: flex;
            justify-content: space-between;
            align-items: center;
            background: linear-gradient(90deg, #f8b4c4, #e07a9a);
            padding: 15px 25px;
            color: #fff;
            box-shadow: 0 4px 12px rgba(153, 63, 95, 0.15);
            border-radius: 0 0 18px 18px;
            margin-bottom: 30px;
        }

        .header-principal .A {
            display: flex;
            align-items: center;
            gap: 12px;
            font-size: 1.6rem;
            font-weight: bold;
            color: #ffffffff; 
            font-family: 'power', serif;
        }

        .header-principal .A-img {
            height: 50px;
            width: 50px;
            border-radius: 50%;
            object-fit: cover;
            border: 2px solid #ffe4ea;
        }

        .nav-usuario {
            display: flex;
            align-items: center;
            gap: 15px;
        }

        .nav-usuario .msg {
            font-family: 'gelo', serif;
            font-weight: bold;
            color: #9b3f5f;
            background-color: #ffe6f0;
            padding: 6px 12px;
            border-radius: 8px;
            font-size: 1.1em;
        }
        
        .btn-logout {
            font-family: 'fogo', serif;
            background: #9b3f5f;
            color: white;
            padding: 8px 16px; 
            border-radius: 8px;
            text-decoration: none;
            transition: background 0.2s ease, transform 0.2s ease;
        }
        
        .btn-logout:hover {
            background: #7a324a;
            transform: translateY(-1px);
        }

        .painel-container {
            max-width: 1300px;
            margin: 40px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 20px;
            box-shadow: 0 15px 40px rgba(155, 63, 95, 0.1);
        }

        .painel-topo {
            text-align: center;
            padding-bottom: 20px;
            border-bottom: 3px solid #ffd1df;
            margin-bottom: 40px;
        }

        .painel-topo h2 {
            font-weight: normal;
            font-family: 'power', serif;
            color: #9b3f5f;
            margin-bottom: 5px;
            font-size: 2.5em;
        }

        .painel-topo p {
            font-family: 'gelo', serif;
            font-size: 1.2em;
            color: #7a5d65;
            font-weight: normal;
        }

        .painel-resumo {
            display: flex;
            flex-wrap: wrap;
            gap: 25px;
            justify-content: center;
            margin-bottom: 50px;
        }

        .card-resumo {
            flex: 1 1 250px;
            background: #fff;
            padding: 25px;
            border-radius: 15px;
            box-shadow: 0 8px 20px rgba(155, 63, 95, 0.1);
            text-align: center;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            position: relative;
            overflow: hidden;
            border: 1px solid #ffb6c1;
        }

        .card-resumo:hover {
            transform: translateY(-8px);
            box-shadow: 0 15px 35px rgba(155, 63, 95, 0.25);
        }

        .card-resumo h3 {
            font-weight: normal;
            font-family: 'power', serif;
            font-size: 1.3em;
            margin: 0 0 10px;
            color: #9b3f5f;
        }

        .card-resumo span {
            font-weight: normal;
            font-family: 'gelo', serif;
            font-size: 2.8em;
            font-weight: bold;
            display: block;
            color: #e07a9a; 
        }
        

        .card-resumo::before {
            content: '';
            position: absolute;
            height: 6px;
            width: 100%;
            top: 0;
            left: 0;
            background: linear-gradient(90deg, var(--cor-faixa-1), var(--cor-faixa-2));
        }

        .card-resumo.azul { --cor-faixa-1: #f8b4c4; --cor-faixa-2: #e07a9a; }
        .card-resumo.verde { --cor-faixa-1: #ffb6c1; --cor-faixa-2: #c95a7e; }
        .card-resumo.roxo { --cor-faixa-1: #ffd1df; --cor-faixa-2: #9b3f5f; }
        .card-resumo.laranja { --cor-faixa-1: #ffe4e9; --cor-faixa-2: #ff7797; }
        .card-resumo.rosa { --cor-faixa-1: #ffc8d6; --cor-faixa-2: #e83e8c; }
        .card-resumo.cinza { --cor-faixa-1: #f2f2f2; --cor-faixa-2: #7a5d65; }
        .card-resumo.cinza span { color: #7a5d65; }

        .painel-menu {
            margin-bottom: 50px;
            padding: 20px;
            background-color: #fff0f5; 
            border-radius: 15px;
            border: 1px solid #ffb6c1;
            box-shadow: 0 4px 15px rgba(155, 63, 95, 0.05);
        }

        .painel-menu h2 {
            font-weight: normal;
            font-family: 'power', serif;
            color: #9b3f5f;
            font-size: 1.6em;
            margin-top: 0;
            border-bottom: 1px dashed #ffb6c1;
            padding-bottom: 10px;
        }


        .painel-menu ul {
            list-style: none;
            padding: 0;
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            justify-content: center;
        }

        .painel-menu li a {
            font-weight: normal;
            font-family: 'gelo', serif;
            text-decoration: none;
            background: linear-gradient(90deg, #e07a9a, #c95a7e);
            color: white;
            padding: 10px 20px;
            border-radius: 25px;
            transition: background 0.3s ease, transform 0.2s;
            font-weight: bold;
            display: flex;
            align-items: center;
            gap: 8px;
        }

        .painel-menu li a:hover {
            background: linear-gradient(90deg, #c95a7e, #b04b6c);
            transform: translateY(-3px);
            box-shadow: 0 4px 10px rgba(155, 63, 95, 0.3);
        }

        .painel-tabela {
            margin-bottom: 50px;
            padding-top: 20px;
        }

        .painel-tabela h2 {
            font-weight: normal;
            font-family: 'power', serif;
            color: #9b3f5f;
            font-size: 2.2em;
            margin-bottom: 20px;
            text-align: center;
        }

        .table-container {
            overflow-x: auto;
            border: 1px solid #ffd1df;
            border-radius: 15px;
            box-shadow: 0 8px 25px rgba(155, 63, 95, 0.1);
        }

        table {
            width: 100%;
            min-width: 1000px; 
            border-collapse: collapse;
            background-color: #fff;
        }

        thead tr {
            background: linear-gradient(90deg, #f8b4c4, #e07a9a);
            color: white;
        }

        th, td {
            padding: 14px 16px;
            border-bottom: 1px solid #ffd1df;
            vertical-align: middle;
            font-size: 1.05em;
            text-align: center;
        }

        th {
            font-weight: normal;
            font-family: 'power', serif;
            font-size: 1.1em;
            letter-spacing: 0.5px;
        }
        
        td {
            font-weight: normal;
            font-family: 'gelo', serif;
            color: #5a3d46;
        }


        tbody tr:nth-child(even) {
            background-color: #fff8fb;
        }

        tbody tr:hover {
            background-color: #ffeff4;
        }

        .btn-email {
            font-weight: normal;
            font-family: 'fogo', serif;
            display: inline-flex;
            align-items: center;
            gap: 5px;
            padding: 8px 15px;
            background: linear-gradient(90deg, #ffc8d6, #ff9ec0); 
            color: #9b3f5f;
            text-decoration: none;
            border-radius: 20px;
            font-weight: bold;
            transition: all 0.2s ease;
            border: 1px solid #ff9ec0;
        }

        .btn-email:hover {
            font-weight: normal;
            background: #ff9ec0;
            color: white;
            transform: translateY(-2px);
            box-shadow: 0 4px 10px rgba(155, 63, 95, 0.4);
        }

        .valor-receita { color: #2ecc71; font-weight: bold; }
        .valor-despesa { color: #e07a9a; font-weight: bold; }

        /* Responsividade */
        @media (max-width: 992px) {
            .painel-container { margin: 20px 10px; }
            .painel-resumo { gap: 15px; }
            .card-resumo { flex: 1 1 45%; }
            .header-principal { flex-direction: column; align-items: flex-start; gap: 10px;}
            .nav-usuario { padding-left: 0; }
            table { min-width: 800px; }
            th, td { padding: 10px 8px; font-size: 0.95em; }
        }

        @media (max-width: 576px) {
            .painel-resumo { flex-direction: column; align-items: stretch; }
            .card-resumo { max-width: 100%; }
            .painel-menu ul { flex-direction: column; }
            .painel-menu li a { justify-content: center; }
        }

        .status-Pendente { color: #ff7797; font-weight: bold; }
        .status-Confirmado { color: #2ecc71; font-weight: bold; }
        .status-Cancelado { color: #9b3f5f; font-weight: bold; }
        footer {
        background: linear-gradient(90deg, #f8b4c4, #e07a9a);
        color: white;
        text-align: center;
        padding: 15px 10px;
        position: fixed;
        bottom: 0;
        left: 0;
        width: 100%;
        font-size: 0.9rem;
        letter-spacing: 0.5px;
        box-shadow: 0 -2px 5px rgba(253, 86, 178, 0.1);
        z-index: 10;
        }

    </style>
</head>
<body class="CorpoSerenity">
    
<header class="header-principal">
    <div class="A">
        <img src="../IMAGENS/Serenity.jpeg" alt="Logo Serenity Spa" class="A-img">
        <span>Serenity Spa</span>
    </div>
    <div class="nav-usuario">
        <span class="msg">Olá, <?= htmlspecialchars($nome) ?>!</span>
        <a href="logout.php" class="btn-logout" title="Sair do Sistema">
            <i class="fas fa-sign-out-alt"></i> Sair
        </a>
    </div>
</header>

<div class="painel-container">
    <div class="painel-topo">
        <h2><i class="fas fa-chart-bar"></i> Gestão Administrativa</h2>
        <p>Cargo: <?= htmlspecialchars($cargo) ?> | Você está na área de controle total do Serenity Spa.</p>
    </div>

    <section class="painel-resumo">
        <div class="card-resumo azul">
            <h3><i class="fas fa-calendar-alt"></i> Agendamentos</h3>
            <span><?= $totalAgendas ?></span>
        </div>
        <div class="card-resumo verde">
            <h3><i class="fas fa-users"></i> Clientes</h3>
            <span><?= $totalClientes ?></span>
        </div>
        <div class="card-resumo roxo">
            <h3><i class="fas fa-spa"></i> Serviços</h3>
            <span><?= $totalServicos ?></span>
        </div>
        <div class="card-resumo laranja">
            <h3><i class="fas fa-box"></i> Pacotes</h3>
            <span><?= $totalPacotes ?></span>
        </div>
        <div class="card-resumo rosa">
            <h3><i class="fas fa-cubes"></i> Produtos (Estoque)</h3>
            <span><?= $totalProdutos ?></span>
        </div>
        <div class="card-resumo cinza">
            <h3><i class="fas fa-dollar-sign"></i> Saldo Financeiro</h3>
            <span>R$ <?= number_format($saldoFinanceiro, 2, ",", ".") ?></span>
        </div>
    </section>

    <section class="painel-menu">
        <h2>Menu de Acesso Rápido</h2>
        <ul>
            <li><a href="#agendamentos"><i class="fas fa-calendar-check"></i> Agendamentos</a></li>
            <li><a href="#clientes"><i class="fas fa-user-friends"></i> Clientes</a></li>
            <li><a href="#servicos"><i class="fas fa-leaf"></i> Serviços</a></li>
            <li><a href="#pacotes"><i class="fas fa-gift"></i> Pacotes</a></li>
            <li><a href="#produtos"><i class="fas fa-boxes"></i> Produtos</a></li>
            <li><a href="#financeiro"><i class="fas fa-chart-line"></i> Financeiro</a></li>
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
                        <th>Ação</th>
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
                            <td class="status-<?= htmlspecialchars($row['status']) ?>">
                                <?= htmlspecialchars($row['status']) ?>
                            </td>
                            <td>
                                <a 
                                    href="mailto:<?= htmlspecialchars($row['email_cliente']) ?>?subject=Detalhes do seu Agendamento&body=Olá <?= htmlspecialchars($row['nome_cliente']) ?>,%0ASeu agendamento para o serviço <?= htmlspecialchars($row['nome_servico']) ?> (Data: <?= date('d/m/Y', strtotime($row['data'])) ?>) está com status: <?= htmlspecialchars($row['status']) ?>.%0A%0AAtenciosamente,%0ASua equipe Serenity Spa" 
                                    class="btn-email"
                                    title="Enviar Email ao Cliente"
                                >
                                    <i class="fas fa-envelope"></i> Contato
                                </a>
                            </td>
                        </tr>
                    <?php endwhile; ?>
                </tbody>
            </table>
        </div>
    </section>

    <section id="clientes" class="painel-tabela">
        <h2>Clientes Cadastrados</h2>
        <div class="table-container">
            <table class="tabela-agenda">
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Email</th>
                        <th>Telefone</th>
                        <th>Nascimento</th>
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
        <h2>Catálogo de Serviços</h2>
        <div class="table-container">
            <table class="tabela-agenda">
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
        <h2>Pacotes Promocionais</h2>
        <div class="table-container">
            <table class="tabela-agenda">
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Descrição</th>
                        <th>Duração Total</th>
                        <th>Preço Base</th>
                        <th>Valor Extra p/ Pessoa</th>
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
            <table class="tabela-agenda">
                <thead>
                    <tr>
                        <th>Produto</th>
                        <th>Categoria</th>
                        <th>Qtd.</th>
                        <th>Preço Unit.</th>
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
        <h2>Movimentação Financeira</h2>
        <div class="table-container">
            <table class="tabela-agenda">
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
                        <?php 
                            $classe_valor = ($row['tipo_financeiro'] === 'receita') ? 'valor-receita' : 'valor-despesa';
                        ?>
                        <td class="<?= $classe_valor ?>">
                            R$ <?= number_format($row['valor_financeiro'], 2, ",", ".") ?>
                        </td>
                        <td><?= date("d/m/Y", strtotime($row['data_financeiro'])) ?></td>
                    </tr>
                    <?php endwhile; ?>
                </tbody>
            </table>
        </div>
    </section>
</div>

<?php
include __DIR__ ."/includes/footer.php";
?>
<script>
    document.addEventListener('DOMContentLoaded', function() {
        document.querySelectorAll('.painel-menu a').forEach(anchor => {
            anchor.addEventListener('click', function (e) {
                e.preventDefault();
                const target = document.querySelector(this.getAttribute('href'));
                if (target) {
                    target.scrollIntoView({
                        behavior: 'smooth',
                        block: 'start' 
                    });
                }
            });
        });
    });
</script>
</body>
</html>