namespace PaymentApp.API.DTOs
{
    public class CardValidationResponseDto
    {
        public bool IsValid { get; set; } = false;
        public string? Message { get; set; }
    }
}
