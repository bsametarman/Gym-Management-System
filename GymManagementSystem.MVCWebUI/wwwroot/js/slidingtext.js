document.addEventListener("DOMContentLoaded", function () {
    let slidingText = document.getElementById("text-sliding");

    setInterval(function () {
        changeColor(slidingText);
    }, 1000);
});

function changeColor(element) {
    let currentColor = window.getComputedStyle(element).color;
    let colorToChange = (currentColor === "rgb(150, 14, 24)") ? "rgb(52, 47, 109)" : "rgb(150, 14, 24)";

    element.style.color = colorToChange;
}