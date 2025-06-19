using Microsoft.EntityFrameworkCore;
using PaymentApp.API.Data;
using PaymentApp.API.DTOs;
using PaymentApp.API.Models;

namespace PaymentApp.API.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transactions tx)
        {
            await _context.Transactions.AddAsync(tx);
        }

        public async Task AddRefundTransactionAsync(string transactionId, string refundCode)
        {
            var refundRequest = new RefundRequest
            {
                TransactionId = transactionId,
                RefundCode = refundCode,
                CreatedAt = DateTime.UtcNow
            };

            await _context.RefundRequests.AddAsync(refundRequest);
        }

        public async Task UpdateAsync(Transactions tx)
        {
            _context.Transactions.Update(tx);
        }

        public async Task<Transactions?> GetByTransactionIdAsync(string transactionId)
        {
            return await _context.Transactions.AsNoTracking().FirstOrDefaultAsync(t => t.TransactionId == transactionId);
        }

        public async Task<List<Transactions>> GetUnconfirmedTransactionsAsync()
        {
            return await _context.Transactions.AsNoTracking().Where(t => !t.IsConfirmed).ToListAsync();
        }

        public async Task<List<PaymentReportResultDto>> GetFilteredAsync(PaymentReportFilterDto filter)
        {
            var query = _context.Transactions.AsNoTracking().AsQueryable();

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

            return await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(t => new PaymentReportResultDto
                {
                    TransactionId = t.TransactionId,
                    CardNumber = t.CardNumber,
                    Amount = t.Amount.Value,
                    IsConfirmed = t.IsConfirmed,
                    IsRefunded = t.IsRefunded,
                    CreatedAt = t.CreatedAt.Value
                })
                .ToListAsync();
        }

        public async Task<List<CardBalanceReportDto>> GetCardBalancesAsync(CardBalanceReportFilterDto filter)
        {
            var query = _context.Transactions.AsNoTracking()
                .Where(t => t.IsConfirmed && !t.IsRefunded);

            if (!string.IsNullOrEmpty(filter.CardNumber))
                query = query.Where(t => t.CardNumber == filter.CardNumber);

            var grouped = query
    .GroupBy(t => t.CardNumber)
    .Select(g => new CardBalanceReportDto
    {
        CardNumber = g.Key,
        TotalSpent = g.Sum(t => t.Amount.Value),
        RemainingBalance = 10000 - g.Sum(t => t.Amount.Value)
    });

            if (filter.MinBalance.HasValue)
                grouped = grouped.Where(r => r.RemainingBalance >= filter.MinBalance.Value);

            if (filter.MaxBalance.HasValue)
                grouped = grouped.Where(r => r.RemainingBalance <= filter.MaxBalance.Value);

            return await grouped
    .Skip((filter.PageNumber - 1) * filter.PageSize)
    .Take(filter.PageSize)
    .ToListAsync();
        }

        public async Task<PaymentSummaryDto> GetPaymentSummaryAsync()
        {
            var transactions = _context.Transactions.AsNoTracking();

            var summary = new PaymentSummaryDto
            {
                TotalPayments = await transactions.CountAsync(),
                ConfirmedCount = await transactions.CountAsync(t => t.IsConfirmed && !t.IsRefunded),
                RefundedCount = await transactions.CountAsync(t => t.IsRefunded),
                HeldCount = await transactions.CountAsync(t => !t.IsConfirmed && !t.IsRefunded),
                TotalAmount = await transactions.SumAsync(t => t.Amount ?? 0)
            };

            return summary;
        }

        public async Task<List<PaymentsTrendDto>> GetPaymentsTrendAsync(DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Transactions.AsNoTracking();

            if (startDate.HasValue)
                query = query.Where(t => t.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.CreatedAt <= endDate.Value);

            return await query
                .GroupBy(t => t.CreatedAt.Value.Date)
                .Select(g => new PaymentsTrendDto
                {
                    Date = g.Key,
                    TotalAmount = g.Sum(x => x.Amount ?? 0)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<PaymentStatusPieDto>> GetPaymentStatusPieAsync()
        {
            var data = new List<PaymentStatusPieDto>
            {
                new PaymentStatusPieDto
                {
                    Status = "Confirmed",
                    Count = await _context.Transactions.CountAsync(t => t.IsConfirmed && !t.IsRefunded)
                },
                new PaymentStatusPieDto
                {
                    Status = "Refunded",
                    Count = await _context.Transactions.CountAsync(t => t.IsRefunded)
                },
                new PaymentStatusPieDto
                {
                    Status = "Held",
                    Count = await _context.Transactions.CountAsync(t => !t.IsConfirmed && !t.IsRefunded)
                }
            };

            return data;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
