import React, { useState } from 'react';
import apiClient from '../api/apiClient';

function PaymentForm() {
    const [form, setForm] = useState({
        cardNumber: '',
        expiry: '',
        cvv: '',
        amount: ''
    });

    const [message, setMessage] = useState('');
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsSubmitting(true);
        setMessage('');

        try {
            // Step 1: Card validation
            const validationResponse = await apiClient.post('/card/validate', {
                cardNumber: form.cardNumber,
                expiry: form.expiry,
                cvv: form.cvv
            });

            if (!validationResponse.data.isValid) {
                setMessage('Card is not valid.');
                return;
            }

            // Step 2: Payment Processing
            const paymentResponse = await apiClient.post('/payment/process', {
                cardNumber: form.cardNumber,
                amount: parseFloat(form.amount)
            });

            setMessage(
                `Payment successful! Transaction ID: ${paymentResponse.data.transactionId}`
            );
        } catch (error) {
            console.error(error);
            setMessage('Payment failed. Please try again.');
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <div className="card shadow-sm mb-4">
            <div className="card-header bg-primary text-white">
                <h5 className="mb-0">Make a Payment</h5>
            </div>
            <div className="card-body">
                <form onSubmit={handleSubmit} className="row g-3">
                    <div className="col-md-6">
                        <label className="form-label">Card Number</label>
                        <input
                            type="text"
                            name="cardNumber"
                            className="form-control"
                            value={form.cardNumber}
                            onChange={handleChange}
                            required
                            maxLength={16}
                        />
                    </div>
                    <div className="col-md-3">
                        <label className="form-label">Expiry (MM/YY)</label>
                        <input
                            type="text"
                            name="expiry"
                            className="form-control"
                            value={form.expiry}
                            onChange={handleChange}
                            required
                            placeholder="12/25"
                        />
                    </div>
                    <div className="col-md-3">
                        <label className="form-label">CVV</label>
                        <input
                            type="password"
                            name="cvv"
                            className="form-control"
                            value={form.cvv}
                            onChange={handleChange}
                            required
                            maxLength={4}
                        />
                    </div>
                    <div className="col-md-4">
                        <label className="form-label">Amount (AED)</label>
                        <input
                            type="number"
                            name="amount"
                            className="form-control"
                            value={form.amount}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="col-12">
                        <button
                            type="submit"
                            className="btn btn-success"
                            disabled={isSubmitting}
                        >
                            {isSubmitting ? 'Processing...' : 'Submit Payment'}
                        </button>
                    </div>
                </form>

                {message && (
                    <div className="alert alert-info mt-3" role="alert">
                        {message}
                    </div>
                )}
            </div>
        </div>
    );
}

export default PaymentForm;
