namespace PaymentApp.API.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public string RefundCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsConfirmed { get; set; } = false;
    }
}
