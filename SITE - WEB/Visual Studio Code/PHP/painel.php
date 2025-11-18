<?php
include __DIR__ . '/includes/header.php';
include __DIR__ . '/database.php';

session_start();

if (!isset($_SESSION['usuario'])) {
    header('Location: index.php');
    die();
}
?>
<header>
    <?php echo "<h1>" . htmlspecialchars($_SESSION['usuario']['nome']) . "</h1>"; ?>
    <button><a href="logout.php">Logout</a></button>
</header>
<main>
    <section>
        <?php
        $dados = $conn->prepare("SELECT a.id_ag, a.desc_ag, a.nome_ag, a.data_ini_ag, a.dia_ag 
                                FROM agenda AS a 
                                INNER JOIN usuario AS u ON u.id_user = a.id_user 
                                WHERE u.id_user = :id;");
        $dados->bindParam(':id', $_SESSION['usuario']['id']);
        $dados->execute();
        $respostas = $dados->fetchAll(PDO::FETCH_OBJ);

        echo "<table border='1'><thead><tr><th>Nome</th><th>Descrição</th><th>Data</th><th>Dias</th><th>Alterar</th><th>Excluir</th></tr></thead><tbody>";
        foreach ($respostas as $linha) {
            echo "<tr><td>" . htmlspecialchars($linha->nome_ag) . "</td><td>" . htmlspecialchars($linha->desc_ag) . "</td><td>" . htmlspecialchars($linha->data_ini_ag) . "</td><td>" . htmlspecialchars($linha->dia_ag) . "</td>";
            echo "<td><a href='alterar.php?id=" . $linha->id_ag . "'>Alterar</a></td>";
            echo "<td><a href='excluir.php?id=" . $linha->id_ag . "'>Excluir</a></td></tr>";
        }
        echo "</tbody></table>";
        ?>
    </section>
</main>
<?php
include __DIR__ . '/includes/footer.php'; 
?>
