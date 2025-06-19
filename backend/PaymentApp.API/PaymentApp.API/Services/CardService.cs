using PaymentApp.API.DTOs;
using PaymentApp.API.Models;
using PaymentApp.API.Repositories;

namespace PaymentApp.API.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<CardValidationResponseDto> ValidateCardAsync(CardValidationRequestDto request)
        {
            var card = await _cardRepository.GetValidCardAsync(
                request.CardNumber,
                request.CVV,
                request.ExpiryDate
            );

            if (card == null)
                return new CardValidationResponseDto { IsValid = false, Message = "Card not found or invalid" };

            if (!card.IsActive)
                return new CardValidationResponseDto { IsValid = false, Message = "Card is inactive" };

            if (!card.ExpiryDate.HasValue || card.ExpiryDate.Value.Date < DateTime.UtcNow.Date)
            {
                return new CardValidationResponseDto { IsValid = false, Message = "Card expired" };
            }


            return new CardValidationResponseDto { IsValid = true, Message = "Card is valid" };
        }
    }
}
