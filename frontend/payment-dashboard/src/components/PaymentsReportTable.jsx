import React, { useEffect, useState } from 'react';
import apiClient from '../api/apiClient';

function PaymentsReportTable() {
  const [payments, setPayments] = useState([]);
  const [filters, setFilters] = useState({
    cardNumber: '',
    status: '',
    startDate: '',
    endDate: '',
    pageNumber: 1,
    pageSize: 5
  });

  const fetchPayments = async () => {
    try {
      const response = await apiClient.get('/reports/payments', { params: filters });
      setPayments(response.data);
    } catch (error) {
      console.error('Error fetching payments:', error);
    }
  };

  useEffect(() => {
    fetchPayments();
  }, [filters]);

  const handleChange = (e) => {
    setFilters({ ...filters, [e.target.name]: e.target.value, pageNumber: 1 });
  };

  return (
    <div className="mb-5">
      <h5 className="mb-3">Payments Report</h5>

      <div className="row g-2 mb-3">
        <div className="col-md-3">
          <input className="form-control" name="cardNumber" placeholder="Card Number" value={filters.cardNumber} onChange={handleChange} />
        </div>
        <div className="col-md-2">
          <select className="form-select" name="status" value={filters.status} onChange={handleChange}>
            <option value="">All</option>
            <option value="confirmed">Confirmed</option>
            <option value="refunded">Refunded</option>
            <option value="held">Held</option>
          </select>
        </div>
        <div className="col-md-2">
          <input type="date" className="form-control" name="startDate" value={filters.startDate} onChange={handleChange} />
        </div>
        <div className="col-md-2">
          <input type="date" className="form-control" name="endDate" value={filters.endDate} onChange={handleChange} />
        </div>
        <div className="col-md-2">
          <button className="btn btn-primary w-100" onClick={fetchPayments}>Filter</button>
        </div>
      </div>

      <table className="table table-bordered table-striped">
        <thead className="table-light">
          <tr>
            <th>Transaction ID</th>
            <th>Card Number</th>
            <th>Amount</th>
            <th>Confirmed</th>
            <th>Refunded</th>
            <th>Created At</th>
          </tr>
        </thead>
        <tbody>
          {payments.length > 0 ? (
            payments.map((t) => (
              <tr key={t.transactionId}>
                <td>{t.transactionId}</td>
                <td>{t.cardNumber}</td>
                <td>{t.amount}</td>
                <td>{t.isConfirmed ? 'Yes' : 'No'}</td>
                <td>{t.isRefunded ? 'Yes' : 'No'}</td>
                <td>{new Date(t.createdAt).toLocaleString()}</td>
              </tr>
            ))
          ) : (
            <tr><td colSpan="6">No results found</td></tr>
          )}
        </tbody>
      </table>

      <div className="d-flex justify-content-between align-items-center">
        <button className="btn btn-outline-secondary" disabled={filters.pageNumber === 1}
          onClick={() => setFilters((prev) => ({ ...prev, pageNumber: Math.max(prev.pageNumber - 1, 1) }))}>
          Prev
        </button>
        <span>Page: {filters.pageNumber}</span>
        <button className="btn btn-outline-secondary"
          onClick={() => setFilters((prev) => ({ ...prev, pageNumber: prev.pageNumber + 1 }))}>
          Next
        </button>
      </div>
    </div>
  );
}

export default PaymentsReportTable;
