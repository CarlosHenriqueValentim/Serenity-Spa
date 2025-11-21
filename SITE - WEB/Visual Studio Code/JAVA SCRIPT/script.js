document.addEventListener("DOMContentLoaded", () => {

  animacaoMenu();
  animacaoLinksSuaves();
  animacaoScroll();
  controleDataHora();

});

function animacaoMenu() {
  const navButtons = document.querySelectorAll("nav a");

  navButtons.forEach((btn, i) => {
    btn.style.opacity = "0";
    btn.style.transform = "translateY(-25px) scale(0.95)";
    btn.style.transition = `
      opacity 0.8s cubic-bezier(.25,.46,.45,.94) ${i * 0.10}s,
      transform 0.8s cubic-bezier(.25,.46,.45,.94) ${i * 0.10}s`;

    setTimeout(() => {
      btn.style.opacity = "1";
      btn.style.transform = "translateY(0) scale(1)";
    }, 100);
  });

  navButtons.forEach(btn => {

    btn.addEventListener("mouseenter", () => {
      btn.style.transform = "scale(1.08)";
      btn.style.textShadow = "0px 0px 6px rgba(255,255,255,0.5)";
    });

    btn.addEventListener("mouseleave", () => {
      btn.style.transform = "scale(1)";
      btn.style.textShadow = "none";
    });

    btn.addEventListener("click", () => {
      btn.style.transform = "scale(0.92)";
      setTimeout(() => btn.style.transform = "scale(1)", 150);
    });

  });
}

function animacaoLinksSuaves() {
  const links = document.querySelectorAll(".link-suave");
  const overlay = document.getElementById("overlay");

  links.forEach(link => {
    link.addEventListener("click", e => {
      e.preventDefault();

      if (overlay) {
        overlay.style.opacity = "1";
        overlay.style.pointerEvents = "all";
      }

      document.body.style.transition = "opacity 0.6s ease";
      document.body.style.opacity = "0";

      setTimeout(() => {
        window.location.href = link.href;
      }, 600);
    });
  });
}

function animacaoScroll() {
  const revelar = document.querySelectorAll(".revelar");

  const observar = new IntersectionObserver(entries => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        entry.target.classList.add("ativo");
      }
    });
  }, { threshold: 0.2 });

  revelar.forEach(el => {
    el.classList.add("esconder");
    observar.observe(el);
  });
}

function controleDataHora() {

  const inputData = document.querySelector('input[name="data_agendamento"]');
  const inputHora = document.querySelector('input[name="hora"]');

  if (!inputData || !inputHora) return;

  const hoje = new Date();
  const dataHoje = hoje.toISOString().split("T")[0];

  inputData.min = dataHoje;

  inputData.addEventListener("change", () => {
    const dataSelecionada = inputData.value;
    const agora = new Date();

    if (dataSelecionada === dataHoje) {
      const horaAtual = formatarHora(agora.getHours(), agora.getMinutes());
      inputHora.min = horaAtual;
    } else {
      inputHora.min = "08:00"; 
    }
  });

}

function formatarHora(h, m) {
  const hora = h < 10 ? "0" + h : h;
  const minuto = m < 10 ? "0" + m : m;
  return `${hora}:${minuto}`;
}

document.addEventListener("DOMContentLoaded", () => {

  const inputData = document.getElementById("dataModern");
  const calendar = document.getElementById("calendar");
  const horaSelect = document.getElementById("horaModern");

  const hoje = new Date();
  let mesAtual = hoje.getMonth();
  let anoAtual = hoje.getFullYear();

  inputData.addEventListener("click", () => {
    calendar.style.display = calendar.style.display === "block" ? "none" : "block";
  });

  renderCalendar(mesAtual, anoAtual);

  function renderCalendar(mes, ano) {
    calendar.innerHTML = "";

    const meses = ["Janeiro","Fevereiro","Março","Abril","Maio","Junho",
                   "Julho","Agosto","Setembro","Outubro","Novembro","Dezembro"];

    const primeiraData = new Date(ano, mes, 1);
    const ultimoDia = new Date(ano, mes + 1, 0).getDate();

    const header = document.createElement("div");
    header.classList.add("calendar-header");

    header.innerHTML = `
      <button onclick="mudarMes(-1)">←</button>
      <span>${meses[mes]} ${ano}</span>
      <button onclick="mudarMes(1)">→</button>
    `;

    const daysContainer = document.createElement("div");
    daysContainer.classList.add("calendar-days");

    for (let i = 1; i <= ultimoDia; i++) {
      const day = document.createElement("div");
      day.classList.add("calendar-day");
      day.innerText = i;

      const dataAtual = new Date(ano, mes, i);

      if (dataAtual < hoje.setHours(0,0,0,0)) {
        day.classList.add("disabled");
      }

      day.addEventListener("click", () => {
        selecionarData(i, mes, ano, day);
      });

      daysContainer.appendChild(day);
    }

    calendar.appendChild(header);
    calendar.appendChild(daysContainer);
  }

  window.mudarMes = function(direcao) {
    mesAtual += direcao;

    if (mesAtual > 11) { mesAtual = 0; anoAtual++; }
    if (mesAtual < 0) { mesAtual = 11; anoAtual--; }

    renderCalendar(mesAtual, anoAtual);
  }

  function selecionarData(dia, mes, ano, element) {
    document.querySelectorAll(".calendar-day").forEach(day => day.classList.remove("selected"));

    element.classList.add("selected");

    const dataFormatada =
      ano + "-" +
      String(mes + 1).padStart(2, "0") + "-" +
      String(dia).padStart(2, "0");

    inputData.value = dataFormatada;

    calendar.style.display = "none";

    gerarHorarios(dataFormatada);
  }

  function gerarHorarios(dataEscolhida) {
    horaSelect.innerHTML = "<option value=''>Selecione um horário</option>";

    const agora = new Date();
    const hojeStr = agora.toISOString().split("T")[0];

    for (let hora = 8; hora <= 19; hora++) {
      ["00", "30"].forEach(min => {

        const horaCompleta = `${String(hora).padStart(2,"0")}:${min}`;

        if (dataEscolhida === hojeStr && hora < agora.getHours()) return;

        const option = document.createElement("option");
        option.value = horaCompleta;
        option.textContent = horaCompleta;

        horaSelect.appendChild(option);
      });
    }
  }

});

document.addEventListener("DOMContentLoaded", function() {

    const dataInput = document.getElementById("data_agendamento");
    const horaInput = document.getElementById("hora_agendamento");

    const hoje = new Date().toISOString().split("T")[0];
    dataInput.min = hoje;

    horaInput.min = "08:00";
    horaInput.max = "18:00";

    horaInput.step = 1800;

    dataInput.addEventListener("change", () => {
        const dia = new Date(dataInput.value).getDay();

        if (dia === 0) {
            alert("Não atendemos aos domingos.");
            dataInput.value = "";
        }
    });

});

flatpickr("#data_agendamento", {
    locale: "pt",
    minDate: "today",
    dateFormat: "d/m/Y",
    disable: [
        function(date) {
            return date.getDay() === 0;
        }
    ]
});

flatpickr("#hora_agendamento", {
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i",
    time_24hr: true,
    minTime: "08:00",
    maxTime: "18:00"
});

document.addEventListener("DOMContentLoaded", () => {

  const data = document.getElementById("data_agendamento");
  const hora = document.getElementById("hora_agendamento");

  if (!data || !hora) {
    console.error("Inputs de data ou hora NÃO encontrados.");
    return;
  }

  console.log("Script conectado com sucesso!");

  const hoje = new Date().toISOString().split("T")[0];
  data.min = hoje;

  hora.min = "08:00";
  hora.max = "18:00";
  hora.step = 1800; 


  data.addEventListener("change", () => {
    const diaSemana = new Date(data.value).getDay();

    if (diaSemana === 0) {
      alert("Não atendemos aos domingos.");
      data.value = "";
    }
  });

});

const data = document.getElementById("data_nasc");

if (data) {
    data.addEventListener("input", () => {
        let valor = data.value.replace(/\D/g, "");

        if (valor.length > 2) 
            valor = valor.slice(0,2) + "/" + valor.slice(2);
        if (valor.length > 5)
            valor = valor.slice(0,5) + "/" + valor.slice(5,9);

        data.value = valor;
    });
}




