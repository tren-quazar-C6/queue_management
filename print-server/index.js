const express = require('express');
const cors = require('cors');
const { exec } = require('child_process');
const fs = require('fs');

const app = express();
app.use(cors());
app.use(express.json());

app.post('/print', (req, res) => {
    const { fullName, documentNumber, ticketCode } = req.body;
    const content = `==================\n      TURNO       \n==================\nNombre: ${fullName}\nDoc:    ${documentNumber}\nTicket: ${ticketCode}\n==================\n Por favor espere \n     su turno     \n==================\n\n\n`;

    fs.writeFileSync('/tmp/ticket.txt', content);
    exec('lp -d "Printer_USB_Printer_Port" /tmp/ticket.txt', (err) => {
        if (err) return res.status(500).json({ error: err.message });
        res.json({ ok: true });
    });
});

app.listen(9100, () => console.log('Print server running on port 9100'));
