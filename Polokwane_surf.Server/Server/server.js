const express = require('express');
const cors = require('cors');
const app = express();

const PORT = process.env.PORT || 7059;

const corsOptions = {
    origin: [
        'https://polokwanewebsite.netlify.app',
        'http://localhost:5173',
        'https://localhost:5173'
    ]
,
    methods: ['GET', 'POST', 'PUT', 'DELETE', 'OPTIONS'],
    allowedHeaders: ['Content-Type', 'Authorization'],
    optionsSuccessStatus: 204,
};

app.use(cors(corsOptions));
app.use(express.json());

app.options('*', cors(corsOptions));

app.get('/', (req, res) => {
    res.send('Polokwane Surfacing Backend is Running');
});

app.get('/api/hello', (req, res) => {
    res.json({ message: 'Hello from backend!' });
});

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
