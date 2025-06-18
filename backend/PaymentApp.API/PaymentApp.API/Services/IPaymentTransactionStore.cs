using PaymentApp.API.Models;

namespace PaymentApp.API.Services
{
    public interface IPaymentTransactionStore
    {
        List<Transactions> GetUnconfirmedTransactions();
        void Add(Transactions transaction);
        Transactions Get(string transactionId);
    }
}
