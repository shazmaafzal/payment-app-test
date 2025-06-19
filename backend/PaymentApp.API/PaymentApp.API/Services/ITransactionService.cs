using PaymentApp.API.DTOs;

namespace PaymentApp.API.Services
{
    public interface ITransactionService
    {
        Task<PagedResult<PaymentReportResultDto>> GetFilteredAsync(PaymentReportFilterDto filter);
        Task<PagedResult<CardBalanceReportDto>> GetCardBalancesAsync(CardBalanceReportFilterDto filter);
        Task<PaymentSummaryDto> GetPaymentSummaryAsync();
        Task<List<PaymentsTrendDto>> GetPaymentsTrendAsync(DateTime? startDate, DateTime? endDate);
        Task<List<PaymentStatusPieDto>> GetPaymentStatusPieAsync();
    }
}
