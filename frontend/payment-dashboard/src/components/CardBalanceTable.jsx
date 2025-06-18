import React, { useEffect, useState } from 'react';
import apiClient from '../api/apiClient';

function CardBalanceTable() {
  const [balances, setBalances] = useState([]);
  const [filters, setFilters] = useState({
    cardNumber: '',
    minBalance: '',
    maxBalance: '',
    pageNumber: 1,
    pageSize: 5
  });

  const fetchCardBalances = async () => {
    try {
      const response = await apiClient.get('/reports/card-balances', {
        params: filters
      });
      setBalances(response.data);
    } catch (error) {
      console.error('Error fetching card balances:', error);
    }
  };

  useEffect(() => {
    fetchCardBalances();
  }, [filters]);

  const handleChange = (e) => {
    setFilters({ ...filters, [e.target.name]: e.target.value, pageNumber: 1 });
  };

  return (
    <div className="mb-5">
      <h5 className="mb-3">Card Balance Report</h5>

      <div className="row g-2 mb-3">
        <div className="col-md-3">
          <input
            className="form-control"
            name="cardNumber"
            placeholder="Card Number"
            value={filters.cardNumber}
            onChange={handleChange}
          />
        </div>
        <div className="col-md-2">
          <input
            className="form-control"
            name="minBalance"
            type="number"
            placeholder="Min Balance"
            value={filters.minBalance}
            onChange={handleChange}
          />
        </div>
        <div className="col-md-2">
          <input
            className="form-control"
            name="maxBalance"
            type="number"
            placeholder="Max Balance"
            value={filters.maxBalance}
            onChange={handleChange}
          />
        </div>
        <div className="col-md-2">
          <button className="btn btn-primary w-100" onClick={fetchCardBalances}>
            Filter
          </button>
        </div>
      </div>

      <table className="table table-bordered table-striped">
        <thead className="table-light">
          <tr>
            <th>Card Number</th>
            <th>Available Balance</th>
          </tr>
        </thead>
        <tbody>
          {balances.length > 0 ? (
            balances.map((b, index) => (
              <tr key={index}>
                <td>{b.cardNumber}</td>
                <td>{b.balance}</td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="2" className="text-center">
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
          onClick={() =>
            setFilters((prev) => ({
              ...prev,
              pageNumber: Math.max(prev.pageNumber - 1, 1)
            }))
          }
        >
          Prev
        </button>
        <span>Page: {filters.pageNumber}</span>
        <button
          className="btn btn-outline-secondary"
          onClick={() =>
            setFilters((prev) => ({
              ...prev,
              pageNumber: prev.pageNumber + 1
            }))
          }
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default CardBalanceTable;
