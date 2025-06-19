namespace PaymentApp.API.DTOs
{
    public class ResponseDto
    {
        public bool IsValid { get; set; } = false;
        public string? Message { get; set; }
    }
}
