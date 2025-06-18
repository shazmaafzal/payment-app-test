import React, { useState } from 'react';
import PaymentForm from './PaymentForm';
import RefundForm from './RefundForm';
import PaymentReportTable from './PaymentsReportTable';
import CardBalanceTable from './CardBalanceTable';

function Dashboard() {
  const [activeTab, setActiveTab] = useState('payment');

  const renderTab = () => {
    switch (activeTab) {
      case 'payment':
        return <PaymentForm />;
      case 'refund':
        return <RefundForm />;
      case 'paymentReport':
        return <PaymentReportTable />;
      case 'cardReport':
        return <CardBalanceTable />;
      default:
        return null;
    }
  };

  return (
    <div className="container mt-4">
      <ul className="nav nav-tabs mb-3">
        <li className="nav-item">
          <button
            className={`nav-link ${activeTab === 'payment' ? 'active' : ''}`}
            onClick={() => setActiveTab('payment')}
          >
            💳 Make Payment
          </button>
        </li>
        <li className="nav-item">
          <button
            className={`nav-link ${activeTab === 'refund' ? 'active' : ''}`}
            onClick={() => setActiveTab('refund')}
          >
            🔁 Refund Payment
          </button>
        </li>
        <li className="nav-item">
          <button
            className={`nav-link ${activeTab === 'paymentReport' ? 'active' : ''}`}
            onClick={() => setActiveTab('paymentReport')}
          >
            📊 Payment Report
          </button>
        </li>
        <li className="nav-item">
          <button
            className={`nav-link ${activeTab === 'cardReport' ? 'active' : ''}`}
            onClick={() => setActiveTab('cardReport')}
          >
            💰 Card Balances
          </button>
        </li>
      </ul>

      {renderTab()}
    </div>
  );
}

export default Dashboard;
