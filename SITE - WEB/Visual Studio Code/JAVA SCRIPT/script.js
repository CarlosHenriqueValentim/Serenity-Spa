document.addEventListener("DOMContentLoaded", () => {

  /* ================================
     ANIMAÇÃO DOS BOTÕES DO MENU
  ================================== */
  const navButtons = document.querySelectorAll("nav a");

  navButtons.forEach((btn, i) => {
    // Estado inicial
    btn.style.opacity = "0";
    btn.style.transform = "translateY(-25px) scale(0.95)";
    btn.style.transition =
      `opacity 0.8s cubic-bezier(.25,.46,.45,.94) ${i * 0.10}s,
       transform 0.8s cubic-bezier(.25,.46,.45,.94) ${i * 0.10}s`;

    // Entrada suave
    setTimeout(() => {
      btn.style.opacity = "1";
      btn.style.transform = "translateY(0) scale(1)";
    }, 150);
  });

  /* ================================
     EFEITO HOVER / CLICK
  ================================== */
  navButtons.forEach(btn => {

    // Hover brilhando
    btn.addEventListener("mouseenter", () => {
      btn.style.transform = "scale(1.08)";
      btn.style.textShadow = "0px 0px 8px rgba(255,255,255,0.7)";
    });

    // Saiu do hover
    btn.addEventListener("mouseleave", () => {
      btn.style.transform = "scale(1)";
      btn.style.textShadow = "none";
    });

    // Clique com pulso
    btn.addEventListener("click", () => {
      btn.style.transform = "scale(0.92)";
      setTimeout(() => {
        btn.style.transform = "scale(1)";
      }, 150);
    });

  });

  /* ================================
     TRANSIÇÃO DE PÁGINAS SUAVE
  ================================== */

  const links = document.querySelectorAll(".link-suave");

  links.forEach(link => {
    link.addEventListener("click", e => {
      e.preventDefault();
      const destino = link.href;

      // overlay (se existir)
      const overlay = document.getElementById("overlay");
      if (overlay) {
        overlay.style.opacity = "1";
        overlay.style.pointerEvents = "all";
        overlay.style.transition = "opacity 0.6s ease";
      }

      // Fade out do corpo
      document.body.style.opacity = "1";
      document.body.style.transition = "opacity 0.6s ease";
      setTimeout(() => {
        document.body.style.opacity = "0";
      }, 50);

      // Navega após o fade
      setTimeout(() => {
        window.location.href = destino;
      }, 650);
    });
  });

  /* ================================
     REVELAÇÃO SUAVE DE ELEMENTOS
     (Scroll Reveal estilo moderno)
  ================================== */

  const revelar = document.querySelectorAll(".revelar");

  const observar = new IntersectionObserver(entries => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        entry.target.style.opacity = "1";
        entry.target.style.transform = "translateY(0)";
      }
    });
  }, { threshold: 0.2 });

  revelar.forEach(el => {
    el.style.opacity = "0";
    el.style.transform = "translateY(30px)";
    el.style.transition = "opacity 0.8s ease, transform 0.8s ease";
    observar.observe(el);
  });

});
