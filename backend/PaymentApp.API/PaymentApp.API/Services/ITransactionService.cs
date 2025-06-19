using PaymentApp.API.DTOs;

namespace PaymentApp.API.Services
{
    public interface ITransactionService
    {
        Task<List<PaymentReportResultDto>> GetFilteredAsync(PaymentReportFilterDto filter);
        Task<List<CardBalanceReportDto>> GetCardBalancesAsync(CardBalanceReportFilterDto filter);
    }
}
