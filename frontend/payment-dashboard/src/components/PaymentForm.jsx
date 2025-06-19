import React, { useState } from 'react';
import apiClient from '../api/apiClient';

function PaymentForm() {
    const [form, setForm] = useState({
        cardNumber: '',
        cardHolderName: '',
        expiry: '',
        cvv: '',
        amount: ''
    });

    const [message, setMessage] = useState('');
    const [isSubmitting, setIsSubmitting] = useState(false);

    // const handleChange = (e) => {
    //     setForm({ ...form, [e.target.name]: e.target.value });
    // };

    const handleChange = (e) => {
        const { name, value } = e.target;

        if (name === "expiry") {
            let digits = value.replace(/\D/g, "");

            if (digits.length > 4) digits = digits.slice(0, 4);

            let formatted = digits;
            if (digits.length >= 3) {
                formatted = `${digits.slice(0, 2)}/${digits.slice(2)}`;
            }

            setForm({ ...form, [name]: formatted });
        } else {
            setForm({ ...form, [name]: value });
        }
    };


    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsSubmitting(true);
        setMessage('');

        try {
            const [month, year] = form.expiry.split('/');

            // if (!month || !year) {
            //     alert("Invalid expiry date format");
            //     return;
            // }

            const expiryDate = new Date(`20${year}-${month}-01T00:00:00Z`);

            // Step 1: Card validation
            const validationResponse = await apiClient.post('/card/ValidateCard', {
                cardNumber: form.cardNumber,
                cardHolderName: form.cardHolderName,
                expiryDate: expiryDate,
                cvv: form.cvv
            });

            if (!validationResponse.data.isValid) {
                setMessage(validationResponse.data.message);
                return;
            }

            // Step 2: Payment Processing
            const paymentResponse = await apiClient.post('/payment/Process', {
                cardNumber: form.cardNumber,
                expiryDate: expiryDate,
                cvv: form.cvv,
                amount: parseFloat(form.amount)
            });

            if (!paymentResponse.data.isValid) {
                setMessage(paymentResponse.data.message);
            }
            else {
                setMessage(`Payment successful! Transaction ID: ${paymentResponse.data.transactionId}`);
            }
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
                    <div className="col-md-6">
                        <label className="form-label">Card Holder Name</label>
                        <input
                            type="text"
                            name="cardHolderName"
                            className="form-control"
                            value={form.cardHolderName}
                            onChange={handleChange}
                            required
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
                            // pattern="^(0[1-9]|1[0-2])\/\d{2}$"
                            maxLength={5}
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
                            maxLength={3}
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
