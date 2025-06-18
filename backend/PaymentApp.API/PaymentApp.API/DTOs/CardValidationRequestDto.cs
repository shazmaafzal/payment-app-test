namespace PaymentApp.API.DTOs
{
    public class CardValidationRequestDto
    {
        public string? CardNumber { get; set; }
        public string? CardHolderName { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
