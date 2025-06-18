using PaymentApp.API.DTOs;
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

        public void Add(Transactions tx)
        {
            _transactions.Add(tx);
        }

        public Transactions Get(string transactionId)
        {
            return _transactions.FirstOrDefault(t => t.TransactionId == transactionId);
        }

        public IEnumerable<PaymentReportResultDto> GetFilteredTransactions(PaymentReportFilterDto filter)
        {
            var query = _transactions.AsQueryable();

            if (!string.IsNullOrEmpty(filter.CardNumber))
                query = query.Where(t => t.CardNumber == filter.CardNumber);

            if (!string.IsNullOrEmpty(filter.Status))
            {
                if (filter.Status == "confirmed")
                    query = query.Where(t => t.IsConfirmed && !t.IsRefunded);
                else if (filter.Status == "refunded")
                    query = query.Where(t => t.IsRefunded);
                else if (filter.Status == "held")
                    query = query.Where(t => !t.IsConfirmed && !t.IsRefunded);
            }

            if (filter.StartDate.HasValue)
                query = query.Where(t => t.CreatedAt >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                query = query.Where(t => t.CreatedAt <= filter.EndDate.Value);

            var paginated = query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(t => new PaymentReportResultDto
                {
                    TransactionId = t.TransactionId,
                    CardNumber = t.CardNumber,
                    Amount = t.Amount,
                    IsConfirmed = t.IsConfirmed,
                    IsRefunded = t.IsRefunded,
                    CreatedAt = t.CreatedAt
                })
                .ToList();

            return paginated;
        }


        public IEnumerable<CardBalanceReportDto> GetCardBalances(CardBalanceReportFilterDto filter)
        {
            var query = _transactions
                .Where(t => t.IsConfirmed && !t.IsRefunded);

            if (!string.IsNullOrEmpty(filter.CardNumber))
            {
                query = query.Where(t => t.CardNumber == filter.CardNumber);
            }

            var grouped = query
                .GroupBy(t => t.CardNumber)
                .Select(g => new CardBalanceReportDto
                {
                    CardNumber = g.Key,
                    TotalSpent = g.Sum(t => t.Amount),
                    RemainingBalance = 10000 - g.Sum(t => t.Amount)
                });

            return grouped
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();
        }
    }
}
