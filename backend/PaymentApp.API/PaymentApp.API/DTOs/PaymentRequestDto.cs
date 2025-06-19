namespace PaymentApp.API.DTOs
{
    public class PaymentRequestDto
    {
        public string? CardNumber { get; set; }
        public decimal? Amount { get; set; }
        public decimal? CVV { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? CardHolderName { get; set; }
    }
}
