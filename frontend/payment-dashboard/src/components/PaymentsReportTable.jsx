import React, { useEffect, useState } from 'react';
import apiClient from '../api/apiClient';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import { PieChart, Pie, Cell, Legend } from 'recharts';

function PaymentsReportTable() {
    const [filters, setFilters] = useState({
        cardNumber: '',
        status: '',
        startDate: '',
        endDate: '',
        pageNumber: 1,
        pageSize: 5
    });

    const [inputValues, setInputValues] = useState({
        cardNumber: '',
        status: '',
        startDate: '',
        endDate: ''
    });

    const [payments, setPayments] = useState([]);
    const [totalCount, setTotalCount] = useState(0);
    const [summary, setSummary] = useState(null);
    const [trendData, setTrendData] = useState([]);
    const [pieData, setPieData] = useState([]);

    const COLORS = ['#0088FE', '#00C49F', '#FFBB28'];

    const buildParams = (customFilters = filters) => {
        const params = {
            cardNumber: customFilters.cardNumber,
            status: customFilters.status,
            pageNumber: customFilters.pageNumber,
            pageSize: customFilters.pageSize,
        };
        if (customFilters.startDate) params.startDate = customFilters.startDate;
        if (customFilters.endDate) params.endDate = customFilters.endDate;
        return params;
    };

    const totalPages = Math.ceil(totalCount / filters.pageSize);

    const fetchPayments = async (customFilters = filters) => {
        try {
            setPayments([]);
            const response = await apiClient.get('/reports/GetPayments', {
                params: buildParams(customFilters),
            });
            setPayments(response.data.items);
            setTotalCount(response.data.totalCount);
        } catch (error) {
            console.error('Error fetching payments:', error);
        }
    };

    const handleChange = (e) => {
        setInputValues({ ...inputValues, [e.target.name]: e.target.value });
    };

    const handleFilterClick = () => {
        const newFilters = { ...inputValues, pageNumber: 1, pageSize: 5 };
        setFilters(newFilters);
        fetchPayments(newFilters);
    };

    const handleResetClick = () => {
        const defaultFilters = {
            cardNumber: '',
            status: '',
            startDate: '',
            endDate: '',
            pageNumber: 1,
            pageSize: 5
        };
        setInputValues({
            cardNumber: '',
            status: '',
            startDate: '',
            endDate: ''
        });
        setFilters(defaultFilters);
        fetchPayments(defaultFilters);
    };

    const goToPage = async (newPageNumber) => {
        const newFilters = { ...filters, pageNumber: newPageNumber };
        await setFilters(newFilters);
        await fetchPayments(newFilters);
    };

    useEffect(() => {
        apiClient.get('/reports/GetPaymentSummary')
            .then(res => setSummary(res.data))
            .catch(err => console.error('Error fetching summary:', err));

        apiClient.get('/reports/GetPaymentsTrend')
            .then(res => setTrendData(res.data))
            .catch(err => console.error('Error fetching trend data:', err));

        apiClient.get('/reports/GetPaymentStatusPie')
            .then(res => setPieData(res.data))
            .catch(err => console.error('Error fetching pie data:', err));

        fetchPayments(filters);
    }, []);

    return (
        <div className="mb-5">
            <h2 className="mb-3">Payments Report</h2>
            <div className="row g-2 mb-3">
                <div className="col-md-3">
                    <label htmlFor="cardNumber" className="form-label">Card Number</label>
                    <input
                        id="cardNumber"
                        className="form-control"
                        name="cardNumber"
                        placeholder="Card Number"
                        value={inputValues.cardNumber}
                        onChange={handleChange}
                    />
                </div>
                <div className="col-md-2">
                    <label htmlFor="status" className="form-label">Status</label>
                    <select
                        id="status"
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
                    <label htmlFor="startDate" className="form-label">Start Date</label>
                    <input
                        type="date"
                        id="startDate"
                        className="form-control"
                        name="startDate"
                        value={inputValues.startDate}
                        onChange={handleChange}
                    />
                </div>
                <div className="col-md-2">
                    <label htmlFor="endDate" className="form-label">End Date</label>
                    <input
                        type="date"
                        id="endDate"
                        className="form-control"
                        name="endDate"
                        value={inputValues.endDate}
                        onChange={handleChange}
                    />
                </div>
                <div className="col-md-3 d-flex align-items-end gap-2">
                    <button className="btn btn-secondary w-40" onClick={handleResetClick}>
                        Reset
                    </button>
                    <button className="btn btn-primary w-50" onClick={handleFilterClick}>
                        Filter
                    </button>
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
                    onClick={() => goToPage(filters.pageNumber - 1)}
                >
                    Prev
                </button>
                <span>Page: {filters.pageNumber} of {totalPages}</span>
                <button
                    className="btn btn-outline-secondary"
                    disabled={filters.pageNumber >= totalPages}
                    onClick={() => goToPage(filters.pageNumber + 1)}
                >
                    Next
                </button>
            </div>

            {summary && (
                <>
                    <h4 className="mt-5 mb-3">Summary</h4>
                    <div className="row mb-4">
                        <div className="col">
                            <div className="card text-bg-light p-3">
                                <strong>Total Payments:</strong> {summary.totalPayments}
                            </div>
                        </div>
                        <div className="col">
                            <div className="card text-bg-light p-3">
                                <strong>Confirmed:</strong> {summary.confirmedCount}
                            </div>
                        </div>
                        <div className="col">
                            <div className="card text-bg-light p-3">
                                <strong>Refunded:</strong> {summary.refundedCount}
                            </div>
                        </div>
                        <div className="col">
                            <div className="card text-bg-light p-3">
                                <strong>Total Amount:</strong> AED {summary.totalAmount}
                            </div>
                        </div>
                    </div>
                </>
            )}

            <h4 className="mt-4 mb-2">Payment Trends (Line Chart)</h4>
            <div className="row g-2 mb-3">
                <ResponsiveContainer width="100%" height={300}>
                    <LineChart data={trendData}>
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis dataKey="date" />
                        <YAxis />
                        <Tooltip />
                        <Line type="monotone" dataKey="totalAmount" stroke="#8884d8" />
                    </LineChart>
                </ResponsiveContainer>
            </div>

            <h4 className="mt-4 mb-2">Payment Status Distribution (Pie Chart)</h4>
            <div className="row g-2 mb-3">
                <PieChart width={300} height={300}>
                    <Pie
                        data={pieData}
                        dataKey="count"
                        nameKey="status"
                        cx="50%"
                        cy="50%"
                        outerRadius={100}
                        fill="#8884d8"
                        label
                    >
                        {pieData.map((_, index) => (
                            <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                        ))}
                    </Pie>
                    <Legend />
                </PieChart>
            </div>
        </div>
    );
}

export default PaymentsReportTable;
