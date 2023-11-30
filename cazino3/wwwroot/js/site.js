const svgWidth = 2410;
const rectWidth = 100;
const moveDistance = 10;
let test = 0;
let value = 0;

const rects = document.querySelectorAll('rect');
let animationStarted = false;
let requestId;

function moveRectangles() {
    if (test <= value) {
        rects.forEach((rect) => {
            let currentX = parseInt(rect.getAttribute('x'));
            const newX = currentX + moveDistance;

            if (newX >= svgWidth) {
                rect.setAttribute('x', -rectWidth);
            } else {
                rect.setAttribute('x', newX);
            }
        });
        test = test + moveDistance;
        requestId = requestAnimationFrame(moveRectangles); // Continue the animation loop
    } else {
        animationStarted = false;
        test = 0;
        setTimeout(() => {
            value = result();
            animateFlow(); // Restart the animation loop after the delay
        }, 5000);
    }
}

function animateFlow() {
    requestId = requestAnimationFrame(moveRectangles); // Start the animation loop
}

function startAnimation() {
    if (!animationStarted) {
        value = result(); // Set value to the result of the function
        animateFlow();
        animationStarted = true;
    }
}

const availableAngles = [
    0, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000, 2100, 2200, 2300, 2400
];

function result() {
    const randomNumber = availableAngles[Math.floor(Math.random() * availableAngles.length)];
    console.log(svgWidth + randomNumber);
    return svgWidth + randomNumber;
}

startAnimation();

