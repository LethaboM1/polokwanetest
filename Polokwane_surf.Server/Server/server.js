const express = require('express');
const cors = require('cors');
const app = express();

const PORT = process.env.PORT || 7059;

const corsOptions = {
    origin: ['https://polokwanewebsite.netlify.app', 'http://localhost:5173'],
};

app.use(cors(corsOptions));
app.use(express.json());

// ? This shows a friendly message at the homepage
app.get('/', (req, res) => {
    res.send('Polokwane Backend is Running ??');
});

// Your test API route
app.get('/api/hello', (req, res) => {
    res.json({ message: 'Hello from backend!' });
});

// Start the server
app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
