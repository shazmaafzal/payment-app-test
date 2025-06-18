using PaymentApp.API.DTOs;
using PaymentApp.API.Models;

namespace PaymentApp.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly List<Card> _cards;
        private readonly List<Transactions> _transactions;
        private readonly IPaymentTransactionStore _transactionStore;

        public PaymentService(IPaymentTransactionStore transactionStore)
        {
            _transactionStore = transactionStore;
            // Use the same in-memory card list or inject from DB later
            _cards = new()
            {
                new Card { CardNumber = "1234567812345678", CardHolderName = "John Doe", ExpiryDate = DateTime.UtcNow.AddYears(1), IsActive = true, Balance = 1000 }
            };

            _transactions = new();
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto request)
        {
            var card = _cards.FirstOrDefault(c => c.CardNumber == request.CardNumber);

            if (card == null || !card.IsActive || card.ExpiryDate < DateTime.UtcNow)
                return new PaymentResponseDto { Message = "Invalid or inactive card" };

            if (card.Balance < request.Amount)
                return new PaymentResponseDto { Message = "Insufficient balance" };

            // Hold amount (just deduct for now, no confirm yet)
            card.Balance -= request.Amount;

            // Generate transaction and refund code
            var transactionId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16); // 16-char
            var refundCode = new Random().Next(1000, 9999).ToString();

            var transaction = new Transactions
            {
                Id = Guid.NewGuid(),
                CardNumber = card.CardNumber,
                Amount = request.Amount,
                TransactionId = transactionId,
                RefundCode = refundCode,
                CreatedAt = DateTime.UtcNow,
                IsConfirmed = false
            };

            //_transactions.Add(transaction);
            _transactionStore.Add(transaction);

            return new PaymentResponseDto
            {
                TransactionId = transaction.TransactionId,
                RefundCode = transaction.RefundCode,
                Message = "Payment processed and held successfully"
            };
        }
    }
}
