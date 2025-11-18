document.addEventListener("DOMContentLoaded", () => {
  
  const navButtons = document.querySelectorAll("nav a");

  navButtons.forEach((btn, i) => {

    btn.style.opacity = "0";
    btn.style.transform = "translateY(-25px) scale(0.95)";
    btn.style.transition =
      `opacity 0.8s cubic-bezier(.25,.46,.45,.94) ${i * 0.10}s,
       transform 0.8s cubic-bezier(.25,.46,.45,.94) ${i * 0.10}s`;

    setTimeout(() => {
      btn.style.opacity = "1";
      btn.style.transform = "translateY(0) scale(1)";
    }, 150);
  });

  navButtons.forEach(btn => {

    btn.addEventListener("mouseenter", () => {
      btn.style.transform = "scale(1.08)";
      btn.style.textShadow = "0px 0px 8px rgba(255,255,255,0.7)";
    });

    btn.addEventListener("mouseleave", () => {
      btn.style.transform = "scale(1)";
      btn.style.textShadow = "none";
    });

    btn.addEventListener("click", () => {
      btn.style.transform = "scale(0.92)";
      setTimeout(() => {
        btn.style.transform = "scale(1)";
      }, 150);
    });

  });
  
  const links = document.querySelectorAll(".link-suave");

  links.forEach(link => {
    link.addEventListener("click", e => {
      e.preventDefault();
      const destino = link.href;

      const overlay = document.getElementById("overlay");
      if (overlay) {
        overlay.style.opacity = "1";
        overlay.style.pointerEvents = "all";
        overlay.style.transition = "opacity 0.6s ease";
      }

      document.body.style.opacity = "1";
      document.body.style.transition = "opacity 0.6s ease";
      setTimeout(() => {
        document.body.style.opacity = "0";
      }, 50);

      setTimeout(() => {
        window.location.href = destino;
      }, 650);
    });
  });
  
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
