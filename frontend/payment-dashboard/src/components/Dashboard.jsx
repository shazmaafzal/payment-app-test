import React, { useState } from 'react';
import PaymentForm from './PaymentForm';
import RefundForm from './RefundForm';
import PaymentReportTable from './PaymentsReportTable';
import CardBalanceTable from './CardBalanceTable';
// import 'bootstrap-icons/font/bootstrap-icons.css';

function Dashboard() {
    const [activeTab, setActiveTab] = useState('paymentReport');
    const [showPaymentModal, setShowPaymentModal] = useState(false);
    const [showRefundModal, setShowRefundModal] = useState(false);

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
        <div className="d-flex flex-column min-vh-100">
            {/* Top Navbar */}
            <nav className="navbar navbar-expand-lg navbar-dark bg-primary px-4 shadow-sm">
                <span className="navbar-brand d-flex align-items-center gap-2">
                    <i className="bi bi-credit-card-2-front-fill"></i>
                    Payment Application
                </span>
            </nav>

            {/* Tabs + Action Buttons */}
            <div className="d-flex justify-content-between align-items-center bg-white px-4 py-3 border-bottom shadow-sm">
                {/* Report Tabs */}
                <ul className="nav nav-tabs border-0">
                    <li className="nav-item">
                        <button
                            className={`nav-link ${activeTab === 'paymentReport' ? 'active' : ''}`}
                            onClick={() => setActiveTab('paymentReport')}
                        >
                            <i className="bi bi-bar-chart-line me-1"></i> Payment Report
                        </button>
                    </li>
                    <li className="nav-item">
                        <button
                            className={`nav-link ${activeTab === 'cardReport' ? 'active' : ''}`}
                            onClick={() => setActiveTab('cardReport')}
                        >
                            <i className="bi bi-wallet2 me-1"></i> Card Balances
                        </button>
                    </li>
                </ul>

                {/* Action Buttons */}
                <div className="d-flex gap-2">
                    <button className="btn btn-success" onClick={() => setShowPaymentModal(true)}>
                        <i className="bi bi-cash-coin me-1"></i> Make Payment
                    </button>
                    <button className="btn btn-warning" onClick={() => setShowRefundModal(true)}>
                        <i className="bi bi-arrow-counterclockwise me-1"></i> Refund
                    </button>
                </div>
            </div>

            {/* Main Content */}
            <div className="flex-grow-1 px-4 py-4">
                <div className="bg-white p-4 shadow-sm rounded">
                    {renderTab()}
                </div>
            </div>

            {/* Payment Modal */}
            {showPaymentModal && (
                <>
                    <div className="modal fade show d-block" tabIndex="-1">
                        <div className="modal-dialog">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title">Make Payment</h5>
                                    <button type="button" className="btn-close" onClick={() => setShowPaymentModal(false)}></button>
                                </div>
                                <div className="modal-body">
                                    <PaymentForm />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="modal-backdrop fade show"></div>
                </>
            )}

            {/* Refund Modal */}
            {showRefundModal && (
                <>
                    <div className="modal fade show d-block" tabIndex="-1">
                        <div className="modal-dialog">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title">Issue Refund</h5>
                                    <button type="button" className="btn-close" onClick={() => setShowRefundModal(false)}></button>
                                </div>
                                <div className="modal-body">
                                    <RefundForm />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="modal-backdrop fade show"></div>
                </>
            )}
        </div>
    );
}

export default Dashboard;
