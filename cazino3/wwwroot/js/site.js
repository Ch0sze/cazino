

const svgWidth = 2500;
const rectWidth = 100;
const moveDistance = 3;

const rects = document.querySelectorAll("rect");

function moveRectangles() {
    rects.forEach((rect) => {
        let currentX = parseInt(rect.getAttribute('x'));

        if (currentX + rectWidth > svgWidth) {
            rect.setAttribute('x', - rectWidth);
        } else {
            const newX = currentX + moveDistance;
            rect.setAttribute('x', newX);
        }
    });
}

// Continuously move rectangles in a smooth loop
function animateFlow() {
    requestAnimationFrame(animateFlow);
    moveRectangles();
}

function calculateResult() {

}

// Start the animation
animateFlow();