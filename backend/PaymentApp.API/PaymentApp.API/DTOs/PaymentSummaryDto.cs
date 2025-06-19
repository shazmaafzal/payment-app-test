namespace PaymentApp.API.DTOs
{
    public class PaymentSummaryDto
    {
        public int TotalPayments { get; set; }
        public int ConfirmedCount { get; set; }
        public int RefundedCount { get; set; }
        public int HeldCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
