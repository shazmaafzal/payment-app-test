using PaymentApp.API.DTOs;

namespace PaymentApp.API.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto request);
    }
}
