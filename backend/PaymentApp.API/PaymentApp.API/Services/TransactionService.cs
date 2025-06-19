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
    }
}
