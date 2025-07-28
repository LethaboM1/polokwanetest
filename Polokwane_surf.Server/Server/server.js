const express = require('express');
const cors = require('cors');
const path = require('path');

const app = express();
const PORT = process.env.PORT || 7059;
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://polokwanetest.onrender.com';

app.listen(PORT, () => {
    console.log(`Server running on port ${PORT}`);
});

app.use(cors());
app.use(express.json());

app.get('/api/hello', (req, res) => {
    res.json({ message: 'Hello from backend!' });
});

app.post('/api/ClientSurvey/submit', (req, res) => {
    const surveyData = req.body;
    console.log('Received survey:', surveyData);

    res.status(200).json({ message: 'Survey submitted successfully' });
});


app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
