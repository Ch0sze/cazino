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
var rolling;
var bets = [];
var UserBetSize = 0;
var UserBetColor = '';
function whereToLand() {
    var roll = Math.floor(Math.random() * 25) + 1;
    return roll;
}


var x2 = 'rgb(235, 221, 35)'
    , x3 = 'rgb(64, 212, 79)'
    , x5 = 'rgb(28, 137, 217)'
    , x8 = 'rgb(153, 51, 255)'
    , x12 = 'rgb(212, 64, 64)';

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
    rolling = true;
    //console.log(rolling);
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
        console.log("barva:" + colors[position + 12]);
    } else console.log("barva:" + colors[position - 13]);

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
        ////
        rolling = false;

        checkWin(position, UserBetSize, UserBetColor);
        UserBetColor = '';
        UserBetSize = 0;
        betOnce = false;
        $('.betText').remove();
    }, 10 * 1000, );
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

function refactorColor(color) {
    switch (color) {
        case 'r':
            return 'rgb(212, 64, 64)';
        case 'g':
            return 'rgb(64, 212, 79)';
        case 'b':
            return 'rgb(28, 137, 217)';
        case 'p':
            return 'rgb(153, 51, 255)';
        case 'y':
            return 'rgb(235, 221, 35)';
        default:
            return null;
    }
}
function checkWin(position, UserBetSize, UserBetColor) {
    var refactoredColor = refactorColor(UserBetColor);
    if (position < 13) {
        if (colors[position + 12] == refactoredColor) {
            console.log("You won");
            addMoney(UserBetSize, UserBetColor);
        } else {
            return;
        }
    } else {
        if (colors[position - 13] == refactoredColor) {
            console.log("You won");
            addMoney(UserBetSize, refactoredColor);
        } else {
            return;
        }
    }
}

function colorToInt(color) {
    switch (color) {
        case 'y':
            return 2;
        case 'g':
            return 3;
        case 'b':
            return 5;
        case 'p':
            return 8;
        case 'r':
            return 20;
        default:
            return "Invalid color";
    }
}

function addMoney(tokenValue, color) {
    color = colorToInt(color);

    tokenValue = tokenValue * color;

    $.ajax({
        url: '/Wallet/BuyTokens',
        type: 'POST',
        data: { tokenValue: tokenValue },
        success: function (result) {
            if (result.success) {
                var balanceLink = document.getElementById('balanceLink');
                var currentBalanceText = balanceLink.textContent;
                var currentBalance = parseInt(currentBalanceText.match(/\d+/)[0]);
                var newBalance = currentBalance + tokenValue;
                console.log("nova hodnota: " + newBalance);
                balanceLink.textContent = 'Your balance: ' + newBalance;
            } else {
                alert(result.message);
            }
        },
        error: function () {
            alert('An error occurred while processing the request.');
        }
    });
}



 function getNickname() {
    // Get the element by its ID
    var manageElement = document.getElementById('manage');

    // Extract the NickName value from the HTML content
    var nickNameText = manageElement.textContent || manageElement.innerText;

    // Extract the NickName value (assuming the format is "Logged as NickName")
    var nickName = nickNameText.replace('Logged as ', '').trim();

    // Now, you can use the nickName in your JavaScript code
    return nickName;
}

function wager(cashInAmount) {
    $.ajax({
        url: '/Wallet/CashIn',
        type: 'POST',
        data: { cashInAmount: cashInAmount },
        success: function (result) {
            if (result.success) {
                if (cashInAmount > 0) {
                    var balanceLink = document.getElementById('balanceLink');

                    // Extract the current balance value
                    var currentBalanceText = balanceLink.textContent;
                    var currentBalance = parseInt(currentBalanceText.match(/\d+/)[0]);

                    // Perform the desired operations
                    var newBalance = currentBalance - cashInAmount;

                    // Update the content of the element
                    balanceLink.textContent = 'Your balance: ' + newBalance;
                }
                else { alert("Not enought money"); return; }

            } else {
                // Handle the case where the cash-in was not successful
                alert(result.message);
            }
        },
        error: function () {
            alert('An error occurred while processing the request.');
            // Handle the case where an error occurred
        }
    });
}
var betOnce;
function bet(color, btn) {
    if (rolling == true || betOnce == true) {
        return; // Exit the function if rolling is true
    }
    betOnce = true;
    var betSize = parseInt(document.getElementById("betInput").value);

    var currentBalanceText = balanceLink.textContent;
    var currentBalance = parseInt(currentBalanceText.match(/\d+/)[0]);
    if (currentBalance < betSize) { alert("Not enought money"); return; }

    wager(betSize);
    UserBetSize = betSize;
    UserBetColor = color;

    var nickName = getNickname();
    var money;

    if (betSize > money || !betSize) {
        console.log('Not enough money for the bet');
        return 0;
    }
    if (color == "r") {
        money -= betSize;
        bets[0] += betSize;
        var element = `<div class="betText"><span class="spacing">${nickName}</span>${betSize.toLocaleString()}</div>`
        $(element).appendTo($(btn).parent().find('.background')).slideUp(1).slideDown(500);
    }
    if (color == "g") {
        money -= betSize;
        bets[1] += betSize;
        var element = `<div class="betText"><span class="spacing">${nickName}</span>${betSize.toLocaleString()}</div>`
        $(element).appendTo($(btn).parent().find('.background')).slideUp(1).slideDown(500);
    }
    if (color == "y") {
        money -= betSize;
        bets[3] += betSize;
        var element = `<div class="betText"><span class="spacing">${nickName}</span>${betSize.toLocaleString()}</div>`
        $(element).appendTo($(btn).parent().find('.background')).slideUp(1).slideDown(500);
    }
    if (color == "b") {
        money -= betSize;
        bets[4] += betSize;
        var element = `<div class="betText"><span class="spacing">${nickName}</span>${betSize.toLocaleString()}</div>`
        $(element).appendTo($(btn).parent().find('.background')).slideUp(1).slideDown(500);
    }
    if (color == "p") {
        money -= betSize;
        bets[5] += betSize;
        var element = `<div class="betText"><span class="spacing">${nickName}</span>${betSize.toLocaleString()}</div>`
        $(element).appendTo($(btn).parent().find('.background')).slideUp(1).slideDown(500);
    }

}
