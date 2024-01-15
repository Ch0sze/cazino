$(document).ready(function () {
    //setup multiple rows of colours, can also add and remove while spinning but overall this is easier.
    initWheel();

    // Run spinWheel() initially
    /*spinWheel(whereToLand());*/

    // Set interval to run spinWheel() every 15 seconds
    //setInterval(function () {
    //    spinWheel(whereToLand());
    //}, 20000);

    const socket = new WebSocket('ws://localhost:8080');
    socket.addEventListener('message', (event) => {
        const num = parseInt(event.data);
        spinWheel(num);
    });
});

function whereToLand() {
    var roll = Math.floor(Math.random() * 25) + 1;
    return roll;
}


var x2 = '#eb8600'
    , x3 = 'green'
    , x5 = 'blue'
    , x8 = 'purple'
    , x12 = 'red';

const colors = [
    x2, x5, x3, x2, x8, x2, x3, x2, x5,
    x2, x3, x2, x12, x2, x3, x2, x5, x2,
    x3, x2, x8, x2, x3, x2, x5,
];

function initWheel() {
    const wheelDiv = document.querySelector('.wheel');


    for (var x = 0; x < 29; x++) {
        for (let i = 0; i < colors.length; i++) {
            const newDiv = document.createElement('div');
            newDiv.classList.add('card');
            newDiv.style.backgroundColor = colors[i];


            wheelDiv.appendChild(newDiv);
        }
    }

}


function spinWheel(roll) {
    var $wheel = $('.roulette_wheel .wheel'),
        order = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25],
        position = order.indexOf(roll);
    console.log(position);

    //determine position where to land
    var rows = 12,
        card = 75 + 3 * 2,
        landingPosition = (rows * 25 * card) + (position * card);

    var randomize = Math.floor(Math.random() * 70) - (70 / 2); //randomize landing position
    //also its 70 because I dont want it to land on the edge of the card

    landingPosition = landingPosition + randomize;

    if (position < 13) {
        console.log(colors[position + 12]);
    } else console.log(colors[position - 13]);

    var object = {
        x: Math.floor(Math.random() * 50) / 100,
        y: Math.floor(Math.random() * 20) / 100
    };

    $wheel.css({
        'transition-timing-function': 'cubic-bezier(0,' + object.x + ',' + object.y + ',1)',
        'transition-duration': '10s',
        'transform': 'translate3d(-' + landingPosition + 'px, 0px, 0px)'
    });

    setTimeout(function () {
        $wheel.css({
            'transition-timing-function': '',
            'transition-duration': '',
        });

        var resetTo = -(position * card + randomize);
        $wheel.css('transform', 'translate3d(' + resetTo + 'px, 0px, 0px)');
        shrinkLine();
    }, 10 * 1000);
}

const timeElement = document.querySelector(".stop_watch_bar");
const animationDuration = 10000; // 10 seconds in milliseconds

function shrinkLine() {
    timeElement.style.width = '100%';
    let start = performance.now();
    const initialWidth = parseFloat(getComputedStyle(timeElement).width);

    function animate(currentTime) {
        const elapsed = currentTime - start;
        const progress = Math.min(elapsed / animationDuration, 1); // Ensure progress doesn't exceed 1

        timeElement.style.width = `${(1 - progress) * initialWidth}px`; // Reduce width gradually

        if (elapsed < animationDuration) {
            requestAnimationFrame(animate); // Continue animation until duration is reached
        }
    }

    requestAnimationFrame(animate); // Start the animation
}

function bet() {
    var betSize = parseFloat(document.getElementById("betInput").value);
    console.log(betSize);
    //if (money < betSize || !betSize) return;

    //if (colors == "#eb8600") {
    //    money -= betSize;
    //    bets[0] += betSize;
    //    var element = `<div class="betText"><span class="name">YOU</span>${betSize.toLocaleString()}</div>`
    //    $(element).appendTo($(btn).parent()).slideUp(1).slideDown(1000);
    //}
}