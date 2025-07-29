import emailjs from '@emailjs/browser';

let API_BASE_URL = '';

if (window.location.hostname === 'localhost') {
    API_BASE_URL = 'https://localhost:7059';
} else {
    API_BASE_URL = 'https://polokwane-server.onrender.com';
}

// Or better (recommended), just use env var:
// const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

const serviceID = import.meta.env.VITE_EMAILJS_SERVICE_ID;
const contactTemplateID = import.meta.env.VITE_EMAILJS_TEMPLATE_ID_CONTACT;
const surveyTemplateID = import.meta.env.VITE_EMAILJS_TEMPLATE_ID_SURVEY;
const publicKey = import.meta.env.VITE_EMAILJS_PUBLIC_KEY;

export function sendContactEmail(formData) {
    return emailjs.send(serviceID, contactTemplateID, formData, publicKey);
}

export function sendSurveyEmail(formData) {
    return emailjs.send(serviceID, surveyTemplateID, formData, publicKey);
}
