using PaymentApp.API.DTOs;

namespace PaymentApp.API.Services
{
    public interface ITransactionService
    {
        Task<List<PaymentReportResultDto>> GetFilteredAsync(PaymentReportFilterDto filter);
        Task<List<CardBalanceReportDto>> GetCardBalancesAsync(CardBalanceReportFilterDto filter);
        Task<PaymentSummaryDto> GetPaymentSummaryAsync();
        Task<List<PaymentsTrendDto>> GetPaymentsTrendAsync(DateTime? startDate, DateTime? endDate);
        Task<List<PaymentStatusPieDto>> GetPaymentStatusPieAsync();
    }
}
