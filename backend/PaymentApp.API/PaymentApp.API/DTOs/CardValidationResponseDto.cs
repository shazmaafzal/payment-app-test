namespace PaymentApp.API.DTOs
{
    public class CardValidationResponseDto
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}
