<?php
session_start();
if(!isset($_SESSION['nome'])) { header("Location: index.php"); exit; }
include __DIR__ . "/header.php";
?>
<h2>Agendamento de Sessão</h2>
<p style="text-align:center; font-weight:bold;">Bem-vindo(a), <?= htmlspecialchars($_SESSION['nome']); ?>!</p>
<?php if(isset($_SESSION['mensagem'])): ?>
    <p class="mensagem"><?= $_SESSION['mensagem']; unset($_SESSION['mensagem']); ?></p>
<?php endif; ?>
<form action="processa_agendamento.php" method="POST" class="form-container">
    <label>Serviço:</label>
    <select name="servico" required>
        <option value="">Escolha...</option>
        <option value="Aromaterapia">Aromaterapia</option>
        <option value="Shiatsu">Shiatsu</option>
        <option value="Massagem Relaxante">Massagem Relaxante</option>
        <option value="Limpeza de Pele">Limpeza de Pele</option>
    </select>
    <label>Data:</label><input type="date" name="data" required>
    <label>Hora:</label><input type="time" name="hora" required>
    <label>Observações:</label><textarea name="obs" rows="3" placeholder="Alergias ou restrições"></textarea>
    <button type="submit">Agendar</button>
</form>
<?php include __DIR__ . "/footer.php"; ?>
