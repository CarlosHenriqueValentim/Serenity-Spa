
document.addEventListener("DOMContentLoaded", () => {
  const navButtons = document.querySelectorAll("nav a");

  
  navButtons.forEach((btn, i) => {
    btn.style.opacity = "0";
    btn.style.transform = "translateY(-20px)";
    btn.style.transition = `opacity 0.6s ease ${i * 0.08}s, transform 0.6s ease ${i * 0.08}s`;
    setTimeout(() => {
      btn.style.opacity = "1";
      btn.style.transform = "translateY(0)";
    }, 100);
  });

  navButtons.forEach((btn) => {
    btn.addEventListener("mouseenter", () => {
      btn.classList.add("hover-animate");
    });
    btn.addEventListener("mouseleave", () => {
      btn.classList.remove("hover-animate");
    });
  });

  navButtons.forEach((btn) => {
    btn.addEventListener("click", (e) => {
      btn.classList.add("clicked");
      setTimeout(() => btn.classList.remove("clicked"), 250);
    });
  });
});

document.querySelectorAll('.link-suave').forEach(link => {
  link.addEventListener('click', function(e) {
    e.preventDefault(); 
    
    const destino = this.href; 
    document.body.classList.add('fade-out'); 


    setTimeout(() => {
      window.location.href = destino;
    }, 500); 
  });
});

document.querySelectorAll('.link-suave').forEach(link => {
  link.addEventListener('click', function(e) {
    e.preventDefault(); // previne redirecionamento imediato
    const destino = this.href;

    const overlay = document.getElementById('overlay');
    overlay.style.pointerEvents = 'all';
    overlay.style.opacity = 1; // inicia a animação

    setTimeout(() => {
      window.location.href = destino; // depois do fade, redireciona
    }, 500); // tempo igual ao transition do CSS
  });
});
