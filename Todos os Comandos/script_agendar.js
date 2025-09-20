const form = document.getElementById("form-agendar");
const msg = document.getElementById("msg");

form.addEventListener("submit", function(e){
  e.preventDefault();
  const data = Object.fromEntries(new FormData(form).entries());
  msg.textContent = `Obrigado, ${data.nome}! Seu pedido para ${data.servico} em ${data.data} às ${data.hora} foi recebido. Entraremos em contato via WhatsApp.`;
  form.reset();
});
