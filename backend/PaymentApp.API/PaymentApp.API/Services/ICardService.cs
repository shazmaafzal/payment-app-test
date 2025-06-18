using PaymentApp.API.DTOs;

namespace PaymentApp.API.Services
{
    public interface ICardService
    {
        Task<CardValidationResponseDto> ValidateCardAsync(CardValidationRequestDto request);
    }
}
