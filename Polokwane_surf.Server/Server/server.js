const express = require('express');
const cors = require('cors');
const app = express();

const PORT = process.env.PORT || 7059;

const corsOptions = {
    origin: ['https://polokwanewebsite.netlify.app', 'http://localhost:5173'],
    methods: ['GET', 'POST', 'PUT', 'DELETE', 'OPTIONS'],
    allowedHeaders: ['Content-Type', 'Authorization'],
    optionsSuccessStatus: 204,
    //credentials: true // if you don't use cookies/auth, you can remove this
};

app.use(cors(corsOptions));
app.use(express.json());

// Handle OPTIONS method for all routes to enable CORS preflight
app.options('*', cors(corsOptions));

// Routes
app.get('/', (req, res) => {
    res.send('Polokwane S Backend is Running');
});

app.get('/api/hello', (req, res) => {
    res.json({ message: 'Hello from backend!' });
});

// Your other API routes like /api/contact/submit and /api/ClientSurvey/submit go here

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
