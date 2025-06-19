using Microsoft.AspNetCore.Mvc;
using PaymentApp.API.DTOs;
using PaymentApp.API.Models;

namespace PaymentApp.API.Repositories
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transactions tx);
        Task AddRefundTransactionAsync(string transactionId, string refundCode);
        Task UpdateAsync(Transactions tx);
        Task<Transactions?> GetByTransactionIdAsync(string transactionId);
        Task<List<Transactions>> GetUnconfirmedTransactionsAsync();
        Task<PagedResult<PaymentReportResultDto>> GetFilteredAsync(PaymentReportFilterDto filter);
        Task<PagedResult<CardBalanceReportDto>> GetCardBalancesAsync(CardBalanceReportFilterDto filter);
        Task SaveChangesAsync();
        Task<PaymentSummaryDto> GetPaymentSummaryAsync();
        Task<List<PaymentsTrendDto>> GetPaymentsTrendAsync(DateTime? startDate, DateTime? endDate);
        Task<List<PaymentStatusPieDto>> GetPaymentStatusPieAsync();
    }
}
