using PaymentApp.API.DTOs;
using PaymentApp.API.Models;
using PaymentApp.API.Repositories;

namespace PaymentApp.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICardRepository _cardRepository;

        public PaymentService(ITransactionRepository transactionRepository, ICardRepository cardRepository)
        {
            _transactionRepository = transactionRepository;
            _cardRepository = cardRepository;
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto request)
        {
            var card = await _cardRepository.GetValidCardAsync(
                request.CardNumber,
                "", // card holder name is not passed in this method, you may want to change this
                null // expiry date is not checked here
            );

            if (card == null || !card.IsActive || card.ExpiryDate < DateTime.UtcNow)
                return new PaymentResponseDto { Message = "Invalid or inactive card" };

            if (card.Balance < request.Amount)
                return new PaymentResponseDto { Message = "Insufficient balance" };

            card.Balance -= request.Amount;
            await _cardRepository.UpdateAsync(card);

            var transactionId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
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

            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();

            return new PaymentResponseDto
            {
                TransactionId = transaction.TransactionId,
                RefundCode = transaction.RefundCode,
                Message = "Payment processed and held successfully"
            };
        }

        //public async Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto request)
        //{
        //    var card = _cards.FirstOrDefault(c => c.CardNumber == request.CardNumber);

        //    if (card == null || !card.IsActive || card.ExpiryDate < DateTime.UtcNow)
        //        return new PaymentResponseDto { Message = "Invalid or inactive card" };

        //    if (card.Balance < request.Amount)
        //        return new PaymentResponseDto { Message = "Insufficient balance" };

        //    // Hold amount (just deduct for now, no confirm yet)
        //    card.Balance -= request.Amount;

        //    // Generate transaction and refund code
        //    var transactionId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16); // 16-char
        //    var refundCode = new Random().Next(1000, 9999).ToString();

        //    var transaction = new Transactions
        //    {
        //        Id = Guid.NewGuid(),
        //        CardNumber = card.CardNumber,
        //        Amount = request.Amount,
        //        TransactionId = transactionId,
        //        RefundCode = refundCode,
        //        CreatedAt = DateTime.UtcNow,
        //        IsConfirmed = false
        //    };

        //    //_transactions.Add(transaction);
        //    _transactionStore.Add(transaction);

        //    return new PaymentResponseDto
        //    {
        //        TransactionId = transaction.TransactionId,
        //        RefundCode = transaction.RefundCode,
        //        Message = "Payment processed and held successfully"
        //    };
        //}
    }
}
