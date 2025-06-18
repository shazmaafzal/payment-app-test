namespace PaymentApp.API.DTOs
{
    public class PaymentReportResultDto
    {
        public string TransactionId { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsRefunded { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
