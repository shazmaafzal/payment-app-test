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
                request.CardHolderName,
                request.ExpiryDate
            );

            //var card = _cards.Find(c =>
            //    c.CardNumber == request.CardNumber &&
            //    c.CardHolderName.ToLower() == request.CardHolderName.ToLower() &&
            //    c.ExpiryDate.Date == request.ExpiryDate.Date);

    //        var card = _cards.Find(c =>
    //c.CardNumber == request.CardNumber &&
    //c.CardHolderName.ToLower() == request.CardHolderName.ToLower() &&
    //c.ExpiryDate.HasValue &&
    //request.ExpiryDate.HasValue &&
    //c.ExpiryDate.Value.Date == request.ExpiryDate.Value.Date);


            if (card == null)
                return new CardValidationResponseDto { IsValid = false, Message = "Card not found or invalid" };

            if (!card.IsActive)
                return new CardValidationResponseDto { IsValid = false, Message = "Card is inactive" };

            //if (card.ExpiryDate.Date < DateTime.UtcNow.Date)
            //    return new CardValidationResponseDto { IsValid = false, Message = "Card expired" };

            if (!card.ExpiryDate.HasValue || card.ExpiryDate.Value.Date < DateTime.UtcNow.Date)
            {
                return new CardValidationResponseDto { IsValid = false, Message = "Card expired" };
            }


            return new CardValidationResponseDto { IsValid = true, Message = "Card is valid" };
        }
    }
}
