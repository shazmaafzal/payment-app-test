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

    // try {
    //   const response = await apiClient.post('/refund/ProcessRefund', {
    //     transactionId: form.transactionId,
    //     refundCode: form.refundCode
    //   });

    //   if (response.data.success) {
    //     setMessage('Refund processed successfully.');
    //   } else {
    //     setMessage('Refund failed: ' + response.data.message);
    //   }
    // } catch (error) {
    //   console.error(error);
    //   setMessage('Error processing refund.');
    // } finally {
    //   setIsSubmitting(false);
    // }

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
    <div className="card shadow-sm mb-4">
      <div className="card-header bg-danger text-white">
        <h5 className="mb-0">Refund Payment</h5>
      </div>
      <div className="card-body">
        <form onSubmit={handleSubmit} className="row g-3">
          <div className="col-md-6">
            <label className="form-label">Transaction ID</label>
            <input
              type="text"
              name="transactionId"
              className="form-control"
              value={form.transactionId}
              onChange={handleChange}
              required
            />
          </div>
          <div className="col-md-6">
            <label className="form-label">Refund Code</label>
            <input
              type="text"
              name="refundCode"
              className="form-control"
              value={form.refundCode}
              onChange={handleChange}
              required
              maxLength={4}
            />
          </div>
          <div className="col-12">
            <button
              type="submit"
              className="btn btn-outline-danger"
              disabled={isSubmitting}
            >
              {isSubmitting ? 'Processing...' : 'Submit Refund'}
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

export default RefundForm;
