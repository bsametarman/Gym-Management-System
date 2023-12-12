document.addEventListener("DOMContentLoaded", function () {
    let slidingText = document.getElementById("text-sliding");

    setInterval(function () {
        changeColor(slidingText);
    }, 500);
});

function changeColor(element) {
    let currentColor = window.getComputedStyle(element).color;
    let colorToChange = (currentColor === "rgb(255, 244, 79)") ? "rgb(0, 255, 255)" : "rgb(255, 244, 79)";

    element.style.color = colorToChange;
}