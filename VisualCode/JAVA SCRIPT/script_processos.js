const accordions = document.querySelectorAll(".accordion-btn");

accordions.forEach(btn => {
  btn.addEventListener("click", () => {
    const panel = btn.nextElementSibling;
    const open = panel.style.maxHeight;
    accordions.forEach(b => b.nextElementSibling.style.maxHeight = null);
    if(open !== panel.scrollHeight + "px") {
      panel.style.maxHeight = panel.scrollHeight + "px";
    }
  });
});
