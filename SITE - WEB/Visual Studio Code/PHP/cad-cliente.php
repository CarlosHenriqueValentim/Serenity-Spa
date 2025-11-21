<?php
include __DIR__ . '/database.php';

if (!isset($_POST['nome'], $_POST['data_nasc'], $_POST['email'], $_POST['senha'], $_POST['sexo'])) {
    header('Location: form-cad.php?erro=Preencha todos os campos');
    exit;
}

$nome      = trim($_POST['nome']);
$email     = trim($_POST['email']);
$senha     = trim($_POST['senha']);
$telefone  = isset($_POST['telefone']) ? trim($_POST['telefone']) : null;
$sexo      = $_POST['sexo'];
$codigo_empresa = 1;

$dataInput = $_POST['data_nasc'];

if (strpos($dataInput, "/") !== false) {
    $partes = explode("/", $dataInput);

    if (count($partes) === 3) {
        $dia  = $partes[0];
        $mes  = $partes[1];
        $ano  = $partes[2];

        $data = "$ano-$mes-$dia";
    } else {
        header("Location: form-cad.php?erro=Data inválida");
        exit;
    }
} else {
    $data = $dataInput;
}

if (!in_array($sexo, ['m', 'f'])) {
    header('Location: form-cad.php?erro=Gênero inválido');
    exit;
}
$senhaSalvar = $senha;
$verifica = $conn->prepare("SELECT codigo_cliente FROM clientes WHERE email_cliente = :email");
$verifica->bindParam(":email", $email);
$verifica->execute();

if ($verifica->rowCount() > 0) {
    header("Location: form-cad.php?erro=E-mail já está cadastrado.");
    exit;
}

try {
    $stmt = $conn->prepare("
        INSERT INTO clientes
        (codigo_empresa, nome_cliente, data_nasc_cliente, telefone_cliente, email_cliente, senha_cliente, sexo_cliente)
        VALUES
        (:codigo_empresa, :nome, :data_nasc, :telefone, :email, :senha, :sexo)
    ");

    $stmt->bindParam(':codigo_empresa', $codigo_empresa, PDO::PARAM_INT);
    $stmt->bindParam(':nome', $nome);
    $stmt->bindParam(':data_nasc', $data);
    $stmt->bindParam(':telefone', $telefone);
    $stmt->bindParam(':email', $email);
    $stmt->bindParam(':senha', $senhaSalvar);
    $stmt->bindParam(':sexo', $sexo);

    $stmt->execute();

    header("Location: index.php?msg=Cadastrado com sucesso!");
    exit;

} catch (PDOException $e) {
    echo "Erro no cadastro: " . $e->getMessage();
}
?>
