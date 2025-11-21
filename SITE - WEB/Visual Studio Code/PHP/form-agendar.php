<?php
include __DIR__ . "/includes/header.php";
?>

<section class="H">
    <div class="I">
        <form action="cad-agendar.php" method="post">
            <h1>Agende sua Sessão</h1>

            <label>Nome</label>
            <input type="text" name="nome" placeholder="Digite seu Nome" maxlength="100" required><br>

            <label>Email</label>
            <input type="email" name="email" placeholder="Digite seu Email" maxlength="100" required><br>

            <label>WhatsApp</label>
            <input type="tel" name="telefone" placeholder="(11) 99999-9999" maxlength="15" required><br>

            <label>Serviço</label>
            <select name="servico" required>
                <option value="Aromaterapia">Aromaterapia</option>
                <option value="Pedras Quentes">Massagem com pedras quentes</option>
                <option value="Shiatsu">Shiatsu</option>
                <option value="Com Ventosa">Com ventosa</option>
                <option value="Sueca">Sueca</option>
                <option value="Desportiva">Desportiva</option>
                <option value="Esfoliação Completa">Esfoliação completa</option>
                <option value="Envoltório de Argilas, Chocolates ou Algas">Envoltório</option>
                <option value="Limpeza de Pele Completa">Limpeza de pele</option>
                <option value="Hidratação Profunda e Máscaras Faciais Nutritivas">Hidratação facial</option>
                <option value="Massagem Facial">Massagem facial</option>
                <option value="Sauna Seca ou a Vapor">Sauna</option>
                <option value="Pacote">Pacote</option>
                <option value="Banho com Sais de Banho">Banho de sais</option>
            </select><br>

            <label>Data do agendamento</label>
            <input type="date" name="data_agendamento" id="data_agendamento" required><br>

            <label>Horário</label>
            <input type="time" name="hora" id="hora_agendamento" required><br>

            <label>Observações</label>
            <textarea name="obs" rows="4" placeholder="Mensagem"></textarea><br>

            <input type="submit" value="Enviar Agendamento">
            <input type="reset" value="Limpar">
        </form>
    </div>
</section>

<?php
include __DIR__ . "/includes/footer.php";
?>
