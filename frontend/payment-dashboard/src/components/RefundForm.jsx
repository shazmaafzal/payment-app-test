import React, { useState } from 'react';
import apiClient from '../api/apiClient';

function RefundForm() {
    const [form, setForm] = useState({
        transactionId: '',
        refundCode: ''
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
            const response = await apiClient.post('/refund/ProcessRefund', {
                transactionId: form.transactionId,
                refundCode: form.refundCode
            });

            if (response.data.isValid) {
                setMessage('Refund processed successfully.');
            } else {
                setMessage('Refund failed: ' + response.data.message);
            }
        } catch (error) {
            if (error.response) {
                setMessage('Refund failed: ' + error.response.data || 'Unknown error');
            } else {
                setMessage('Error processing refund.');
            }
            console.error(error);
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <div className="card border-0">
            <div className="card-body p-0">
                <form onSubmit={handleSubmit} className="row g-4">
                    <div className="col-md-12">
                        <label className="form-label">Transaction ID</label>
                        <input
                            type="text"
                            name="transactionId"
                            className="form-control"
                            value={form.transactionId}
                            onChange={handleChange}
                            required
                            maxLength={20}
                            placeholder="Enter transaction ID"
                        />
                    </div>
                    <div className="col-md-12">
                        <label className="form-label">Refund Code</label>
                        <input
                            type="text"
                            name="refundCode"
                            className="form-control"
                            value={form.refundCode}
                            onChange={handleChange}
                            required
                            maxLength={4}
                            placeholder="4-digit code"
                        />
                    </div>

                    <div className="col-12 text-end">
                        <button
                            type="submit"
                            className="btn btn-outline-danger"
                            disabled={isSubmitting}
                        >
                            {isSubmitting ? 'Processing...' : 'Submit Refund'}
                        </button>
                    </div>

                    {message && (
                        <div className="col-12">
                            <div className="alert alert-info mb-0" role="alert">
                                {message}
                            </div>
                        </div>
                    )}
                </form>
            </div>
        </div>
    );

}

export default RefundForm;
