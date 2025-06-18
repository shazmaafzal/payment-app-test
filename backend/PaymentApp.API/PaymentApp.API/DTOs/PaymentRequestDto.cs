namespace PaymentApp.API.DTOs
{
    public class PaymentRequestDto
    {
        public string? CardNumber { get; set; }
        public decimal? Amount { get; set; }
    }
}
