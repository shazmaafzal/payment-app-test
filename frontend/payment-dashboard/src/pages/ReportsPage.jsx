import PaymentsReportTable from '../components/PaymentsReportTable';
import CardBalanceTable from '../components/CardBalanceTable';

function ReportsPage() {
  return (
    <div style={{ padding: '20px' }}>
      <h1>Payment Dashboard</h1>

      <section style={{ marginBottom: '3rem' }}>
        <h2>Payments Report</h2>
        <PaymentsReportTable />
      </section>

      <section>
        <h2>Card Balance Report</h2>
        <CardBalanceTable />
      </section>
    </div>
  );
}

export default ReportsPage;
