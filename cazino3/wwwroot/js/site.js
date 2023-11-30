const svgWidth = 2400;
const rectWidth = 100;
const moveDistance = 8;
let test = 0;
let value = 0;

const rects = document.querySelectorAll('rect');
let animationStarted = false;

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
    } else {
        animationStarted = false;
    }
}

function animateFlow() {
    requestAnimationFrame(animateFlow);
    moveRectangles();
}

function startAnimation() {
    if (!animationStarted) {
        value = result(); // Set value to the result of the function
        animateFlow();
        animationStarted = true;
    }
}

// Find the button element
const startButton = document.getElementById('startButton');

// Add a click event listener to start the animation when the button is clicked
startButton.addEventListener('click', () => {
    startAnimation(); // Call startAnimation() when the button is clicked
});

const availableAngles = [
    0, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000, 2100, 2200, 2300, 2400
];

function result() {
    const randomNumber = availableAngles[Math.floor(Math.random() * availableAngles.length)];
    console.log(svgWidth + randomNumber);
    return svgWidth + randomNumber;
}


//function calculateResult() {
//    const rects = document.getElementById('rect');
//    const wheelWidth = 100 * 25 + 100
//    const spinDuration = 3000; //ms
//    const randomPosition = Math.floor(Math.random() + wheelWidth);

//    //reset all transitions
//    wheel.style.transition = 'none';
//    wheel.style.transform = 'translateX(0)';

//    function moveWheel() {
//        rects.forEach(() => {
//            let currentX = parseInt(rect.getAttribute('x'));

//            if (currentX + rectWidth > svgWidth) {
//                rect.setAttribute('x', - rectWidth);
//            } else {
//                const newX = currentX + moveDistance;
//                rect.setAttribute('x', newX);
//            }
//        });
//    }

//    // After the spin duration, calculate the final random position within the wheel
//    setTimeout(() => {

//        // Settle at the final random position
//        wheel.style.transition = 'transform 1s ease-in-out'; // Adjust the landing animation duration and easing as needed
//        wheel.style.transform = `translateX(${randomPosition}px)`;

//        // Do something with the final random position, e.g., determine the selected segment
//        console.log('Selected position:', randomPosition);
//    }, spinDuration);

//}