namespace PaymentApp.API.DTOs
{
    public class PaymentResponseDto
    {
        public string? TransactionId { get; set; }
        public string? RefundCode { get; set; }
        public string? Message { get; set; }
    }
}
