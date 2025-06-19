using Microsoft.AspNetCore.Mvc;
using PaymentApp.API.Models;
using PaymentApp.API.Repositories;
using PaymentApp.API.Services;

namespace PaymentApp.API.Controllers
{
    [ApiController]
    [Route("api/refund")]
    public class RefundController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<RefundController> _logger;

        public RefundController(ITransactionRepository transactionRepository, ILogger<RefundController> logger)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        [HttpPost("ProcessRefund")]
        public async Task<IActionResult> ProcessRefund([FromBody] RefundRequest request)
        {
            var transaction = await _transactionRepository.GetByTransactionIdAsync(request.TransactionId);
            if (transaction == null)
            {
                return NotFound("Transaction not found.");
            }

            if (transaction.RefundCode != request.RefundCode)
            {
                return BadRequest("Invalid refund code.");
            }

            var now = DateTime.UtcNow;

            if (!transaction.CreatedAt.HasValue || now.Date > transaction.CreatedAt.Value.Date)
            {
                return BadRequest("Refund period expired.");
            }

            if (transaction.IsRefunded)
            {
                return BadRequest("Transaction already refunded.");
            }

            transaction.IsRefunded = true;
            transaction.IsConfirmed = false; // if you want to mark refunded as not confirmed

            await _transactionRepository.UpdateAsync(transaction);
            await _transactionRepository.SaveChangesAsync();

            _logger.LogInformation($"Transaction {transaction.TransactionId} refunded at {now}");

            return Ok(new { message = "Refund processed successfully." });
        }
    }
}
