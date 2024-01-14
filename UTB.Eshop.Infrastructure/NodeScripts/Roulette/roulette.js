const WebSocketServer = require('ws').Server;
const wss = new WebSocketServer({ port: 8080 });

wss.on('connection', (ws) => {
    console.log('Client connected
});

setInterval(() => {
    const computerChoice = Math.floor(Math.random() * 70) - (70 / 2);

    wss.clients.forEach((ws) => {
        if (ws.readyState === ws.OPEN) {
            ws.send(computerChoice);
        }
    })
}, 20000);