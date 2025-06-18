using PaymentApp.API.Models;

namespace PaymentApp.API.Services
{
    public class PaymentTransactionStore : IPaymentTransactionStore
    {
        private readonly List<Transactions> _transactions;

        public PaymentTransactionStore()
        {
            _transactions = new List<Transactions>();
        }

        public List<Transactions> GetUnconfirmedTransactions()
        {
            return _transactions.Where(t => !t.IsConfirmed).ToList();
        }

        public void Add(Transactions tx) => _transactions.Add(tx);

        public Transactions Get(string transactionId) =>
            _transactions.FirstOrDefault(t => t.TransactionId == transactionId);
    }
}
