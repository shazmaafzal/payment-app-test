﻿using Microsoft.AspNetCore.Mvc;
using PaymentApp.API.Controllers;
using PaymentApp.API.DTOs;
using PaymentApp.API.Models;
using PaymentApp.API.Repositories;

namespace PaymentApp.API.Services
{
    public class RefundService : IRefundService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ILogger<RefundController> _logger;

        public RefundService(ITransactionRepository transactionRepository, ICardRepository cardRepository, ILogger<RefundController> logger)
        {
            _transactionRepository = transactionRepository;
            _cardRepository = cardRepository;
            _logger = logger;
        }

        public async Task<ResponseDto> ProcessRefund(RefundRequest request)
        {
            var transaction = await _transactionRepository.GetByTransactionIdAsync(request.TransactionId);
            if (transaction == null)
            {
                return new ResponseDto { IsValid = false, Message = "Transaction not found." };
            }

            if (transaction.RefundCode != request.RefundCode)
            {
                return new ResponseDto { IsValid = false, Message = "Invalid refund code." };
            }

            var now = DateTime.UtcNow;

            if (!transaction.CreatedAt.HasValue || now.Date > transaction.CreatedAt.Value.Date)
            {
                return new ResponseDto { IsValid = false, Message = "Refund period expired." };
            }

            if (transaction.IsRefunded)
            {
                return new ResponseDto { IsValid = false, Message = "Transaction already refunded." };
            }

            var card = await _cardRepository.GetByCardNumberAsync(transaction.CardNumber);

            if (card == null)
            {
                return new ResponseDto { IsValid = false, Message = "Card not found for refund." };
            }
            card.Balance += transaction.Amount ?? 0;
            await _cardRepository.UpdateAsync(card);

            transaction.IsRefunded = true;
            transaction.IsConfirmed = false;

            await _transactionRepository.UpdateAsync(transaction);
            await _transactionRepository.AddRefundTransactionAsync(transaction.TransactionId, transaction.RefundCode);
            await _transactionRepository.SaveChangesAsync();

            _logger.LogInformation($"Transaction {transaction.TransactionId} refunded at {now}");

            return new ResponseDto { IsValid = true, Message = "Refund processed successfully." };
        }
    }
}
