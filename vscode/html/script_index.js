
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
