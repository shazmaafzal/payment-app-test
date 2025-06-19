import React, { useEffect, useState } from 'react';
import apiClient from '../api/apiClient';

function CardBalanceTable() {
    const [balances, setBalances] = useState([]);
    const [totalCount, setTotalCount] = useState(0);

    const [filters, setFilters] = useState({
        cardNumber: '',
        minBalance: '',
        maxBalance: '',
        pageNumber: 1,
        pageSize: 5
    });

    const buildParams = (params) => {
        const result = {};
        for (const key in params) {
            const value = params[key];
            if (value !== '' && value !== null && value !== undefined) {
                result[key] = value;
            }
        }
        return result;
    };

    const fetchCardBalances = async (params) => {
        try {
            const response = await apiClient.get('/reports/GetCardBalances', {
                params: buildParams(params)
            });
            setBalances(response.data.items);
            setTotalCount(response.data.totalCount);
        } catch (error) {
            console.error('Error fetching card balances:', error);
        }
    };

    useEffect(() => {
        fetchCardBalances(filters);
    }, [filters]);

    const handleChange = (e) => {
        setFilters({ ...filters, [e.target.name]: e.target.value });
    };

    const handleFilter = () => {
        setFilters(prev => ({ ...prev, pageNumber: 1 }));
    };

    const handleReset = () => {
        const defaultFilters = {
            cardNumber: '',
            minBalance: '',
            maxBalance: '',
            pageNumber: 1,
            pageSize: 5
        };
        setFilters(defaultFilters);
    };

    const totalPages = Math.ceil(totalCount / filters.pageSize);

    const goToPage = (page) => {
        if (page >= 1 && page <= totalPages) {
            setFilters(prev => ({ ...prev, pageNumber: page }));
        }
    };

    return (
        <div className="mb-5">
            <h2 className="mb-3">Card Balance Report</h2>
            <div className="row g-2 mb-3">
                <div className="col-md-3">
                    <label htmlFor="cardNumber" className="form-label">Card Number</label>
                    <input
                        id="cardNumber"
                        className="form-control"
                        name="cardNumber"
                        placeholder="Card Number"
                        value={filters.cardNumber}
                        onChange={handleChange}
                    />
                </div>
                <div className="col-md-2">
                    <label htmlFor="minBalance" className="form-label">Min Balance</label>
                    <input
                        id="minBalance"
                        className="form-control"
                        name="minBalance"
                        type="number"
                        value={filters.minBalance}
                        onChange={handleChange}
                    />
                </div>
                <div className="col-md-2">
                    <label htmlFor="maxBalance" className="form-label">Max Balance</label>
                    <input
                        id="maxBalance"
                        className="form-control"
                        name="maxBalance"
                        type="number"
                        value={filters.maxBalance}
                        onChange={handleChange}
                    />
                </div>
                <div className="col-md-3 d-flex align-items-end gap-2">
                    <button className="btn btn-secondary w-40" onClick={handleReset}>
                        Reset
                    </button>
                    <button className="btn btn-primary w-50" onClick={handleFilter}>
                        Filter
                    </button>
                </div>
            </div>

            <table className="table table-bordered table-striped">
                <thead className="table-light">
                    <tr>
                        <th>Card Number</th>
                        <th>Total Spent</th>
                        <th>Remaining Balance</th>
                    </tr>
                </thead>
                <tbody>
                    {balances.length > 0 ? (
                        balances.map((b, index) => (
                            <tr key={index}>
                                <td>{b.cardNumber}</td>
                                <td>{b.totalSpent}</td>
                                <td>{b.remainingBalance}</td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="3" className="text-center">
                                No results found
                            </td>
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
                <span>
                    Page: {filters.pageNumber} of {totalPages || 1}
                </span>
                <button
                    className="btn btn-outline-secondary"
                    disabled={filters.pageNumber >= totalPages}
                    onClick={() => goToPage(filters.pageNumber + 1)}
                >
                    Next
                </button>
            </div>
        </div>
    );
}

export default CardBalanceTable;
