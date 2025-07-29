import React, { useState } from 'react';
import Header from '../components/Header';
import CarouselSection from '../components/CarouselSection';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import '../App.css';

import { sendSurveyEmail } from '../services/EmailService';


const ClientSurvey = () => {
    const [formData, setFormData] = useState({
        name: '',
        email: '',
        serviceUsed: '',
        rating: '',
        feedback: ''
    });

    const [starRating, setStarRating] = useState(0);
    const [loading, setLoading] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    const handleStarClick = (value) => {
        setStarRating(value);
        setFormData((prev) => ({ ...prev, rating: value.toString() }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (starRating === 0) {
            toast.error('Please select a star rating.');
            return;
        }

        setLoading(true);

        const API_BASE_URL = import.meta.env.PROD
            ? 'https://polokwane-server.onrender.com'
            : 'https://localhost:7059';

        try {
            const dbResponse = await fetch(`${API_BASE_URL}/api/ClientSurvey/submit`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData),
            });


            if (!dbResponse.ok) {
                const errorText = await dbResponse.text();
                throw new Error(errorText || 'Failed to save survey.');
            }

            // Send email via EmailJS
            emailjs.send(
                'service_1ikwxjx',
                'template_azbebum',
                { name, email, serviceUsed, rating, stars, feedback },
                'KhrJ-Dy0fzYUqQKnW'  
            );

            // After successful API submit, send EmailJS email:
            await sendSurveyEmail(formData);

            toast.success('Survey submitted and email sent!');
            setFormData({ name: '', email: '', serviceUsed: '', rating: '', feedback: '' });
            setStarRating(0);
        } catch (error) {
            console.error('Survey error:', error);
            toast.error('Something went wrong. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <Header />
            <CarouselSection />

            <section className="survey section">
                <div className="container">
                    <div className="section-heading d-flex align-items-center gap-3 mb-4">
                        <h4>CLIENT SURVEY</h4>
                        <div className="line"></div>
                    </div>

                    <form className="survey-form" onSubmit={handleSubmit}>
                        <div className="row gy-4">
                            <div className="col-md-6">
                                <input
                                    type="text"
                                    className="form-control"
                                    placeholder="Enter your Full Name"
                                    name="name"
                                    value={formData.name}
                                    onChange={handleChange}
                                    required
                                />
                            </div>
                            <div className="col-md-6">
                                <input
                                    type="email"
                                    className="form-control"
                                    placeholder="Enter your email"
                                    name="email"
                                    value={formData.email}
                                    onChange={handleChange}
                                    required
                                />
                            </div>
                            <div className="col-md-6">
                                <input
                                    type="text"
                                    className="form-control"
                                    placeholder="Service Used"
                                    name="serviceUsed"
                                    value={formData.serviceUsed}
                                    onChange={handleChange}
                                    required
                                />
                            </div>
                            <div className="col-md-6">
                                <div className="star-rating">
                                    {[1, 2, 3, 4, 5].map((star) => (
                                        <span
                                            key={star}
                                            onClick={() => handleStarClick(star)}
                                            style={{
                                                fontSize: '24px',
                                                cursor: 'pointer',
                                                color: star <= starRating ? '#f1c40f' : '#ccc',
                                                fontFamily: 'Arial, sans-serif'
                                            }}
                                            dangerouslySetInnerHTML={{ __html: '&#9733;' }} // Unicode star
                                        ></span>
                                    ))}
                                    <span className="ms-2">
                                        {starRating > 0 ? `${starRating} Star${starRating > 1 ? 's' : ''}` : ''}
                                    </span>
                                </div>
                            </div>
                            <div className="col-12">
                                <textarea
                                    className="form-control"
                                    rows="5"
                                    placeholder="Additional Feedback"
                                    name="feedback"
                                    value={formData.feedback}
                                    onChange={handleChange}
                                    required
                                />
                            </div>
                            <div className="col-12 text-end">
                                <button type="submit" className="btn btn-success px-4" disabled={loading}>
                                    {loading ? 'Submitting...' : 'Submit Survey'}
                                </button>
                            </div>
                        </div>
                    </form>
                </div>
                <ToastContainer position="top-right" autoClose={3000} />
            </section>
        </>
    );
};

export default ClientSurvey;
