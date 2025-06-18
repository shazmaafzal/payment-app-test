using PaymentApp.API.DTOs;
using PaymentApp.API.Models;

namespace PaymentApp.API.Repositories
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transactions tx);
        Task<Transactions?> GetByTransactionIdAsync(string transactionId);
        Task<List<Transactions>> GetUnconfirmedTransactionsAsync();
        Task<List<PaymentReportResultDto>> GetFilteredAsync(PaymentReportFilterDto filter);
        Task<List<CardBalanceReportDto>> GetCardBalancesAsync(CardBalanceReportFilterDto filter);
        Task SaveChangesAsync();
    }
}
