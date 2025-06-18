import React, { useEffect, useState } from 'react';
import apiClient from '../api/apiClient';

function PaymentsReportTable() {
    // This holds the filters sent to the backend
    const [filters, setFilters] = useState({
        cardNumber: '',
        status: '',
        startDate: '',
        endDate: '',
        pageNumber: 1,
        pageSize: 5
    });

    // This holds temporary input values until user clicks filter
    const [inputValues, setInputValues] = useState({
        cardNumber: '',
        status: '',
        startDate: '',
        endDate: ''
    });

    const [payments, setPayments] = useState([]);

    const buildParams = () => {
        const params = {
            cardNumber: filters.cardNumber,
            status: filters.status,
            pageNumber: filters.pageNumber,
            pageSize: filters.pageSize,
        };
        if (filters.startDate) params.startDate = filters.startDate;
        if (filters.endDate) params.endDate = filters.endDate;
        return params;
    };

    const fetchPayments = async () => {
        try {
            const response = await apiClient.get('/reports/GetPayments', { params: buildParams() });
            setPayments(response.data);
        } catch (error) {
            console.error('Error fetching payments:', error);
        }
    };

    // Fetch payments whenever filters change (page number or filters applied)
    useEffect(() => {
        fetchPayments();
    }, [filters]);

    // Update temporary inputs as user types/selects
    const handleChange = (e) => {
        setInputValues({ ...inputValues, [e.target.name]: e.target.value });
    };

    // When filter button clicked, update filters state and reset pageNumber to 1
    const handleFilterClick = () => {
        setFilters({ ...inputValues, pageNumber: 1, pageSize: 5 });
    };

    return (
        <div className="mb-5">
            <h5 className="mb-3">Payments Report</h5>

            <div className="row g-2 mb-3">
                <div className="col-md-3">
                    <input
                        className="form-control"
                        name="cardNumber"
                        placeholder="Card Number"
                        value={inputValues.cardNumber}
                        onChange={handleChange}
                    />
                </div>
                <div className="col-md-2">
                    <select
                        className="form-select"
                        name="status"
                        value={inputValues.status}
                        onChange={handleChange}
                    >
                        <option value="">All</option>
                        <option value="confirmed">Confirmed</option>
                        <option value="refunded">Refunded</option>
                        <option value="held">Held</option>
                    </select>
                </div>
                <div className="col-md-2">
                    <input
                        type="date"
                        className="form-control"
                        name="startDate"
                        value={inputValues.startDate}
                        onChange={handleChange}
                    />
                </div>
                <div className="col-md-2">
                    <input
                        type="date"
                        className="form-control"
                        name="endDate"
                        value={inputValues.endDate}
                        onChange={handleChange}
                    />
                </div>
                <div className="col-md-2">
                    <button className="btn btn-primary w-100" onClick={handleFilterClick}>
                        Filter
                    </button>
                </div>
            </div>

            {/* table and pagination code remains same */}

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
                        <tr>
                            <td colSpan="6">No results found</td>
                        </tr>
                    )}
                </tbody>
            </table>

            <div className="d-flex justify-content-between align-items-center">
                <button
                    className="btn btn-outline-secondary"
                    disabled={filters.pageNumber === 1}
                    onClick={() =>
                        setFilters((prev) => ({
                            ...prev,
                            pageNumber: Math.max(prev.pageNumber - 1, 1),
                        }))
                    }
                >
                    Prev
                </button>
                <span>Page: {filters.pageNumber}</span>
                <button
                    className="btn btn-outline-secondary"
                    onClick={() => setFilters((prev) => ({ ...prev, pageNumber: prev.pageNumber + 1 }))}
                >
                    Next
                </button>
            </div>
        </div>
    );
}

export default PaymentsReportTable;
