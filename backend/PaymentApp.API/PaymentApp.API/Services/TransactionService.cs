using PaymentApp.API.Controllers;
using PaymentApp.API.DTOs;
using PaymentApp.API.Repositories;

namespace PaymentApp.API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<List<PaymentReportResultDto>> GetFilteredAsync(PaymentReportFilterDto filter)
        {
            return await _transactionRepository.GetFilteredAsync(filter);
        }

        public async Task<List<CardBalanceReportDto>> GetCardBalancesAsync(CardBalanceReportFilterDto filter)
        {
            return await _transactionRepository.GetCardBalancesAsync(filter);
        }

        public async Task<PaymentSummaryDto> GetPaymentSummaryAsync()
        {
            return await _transactionRepository.GetPaymentSummaryAsync();
        }

        public async Task<List<PaymentsTrendDto>> GetPaymentsTrendAsync(DateTime? startDate, DateTime? endDate)
        {
            return await _transactionRepository.GetPaymentsTrendAsync(startDate, endDate);
        }

        public async Task<List<PaymentStatusPieDto>> GetPaymentStatusPieAsync()
        {
            return await _transactionRepository.GetPaymentStatusPieAsync();
        }
    }
}
