using PaymentApp.API.DTOs;
using PaymentApp.API.Models;

namespace PaymentApp.API.Services
{
    public class CardService : ICardService
    {
        // For demo: In-memory card list - replace with DB later
        private readonly List<Card> _cards = new()
        {
            new Card {
                Id = 1,
                CardNumber = "1234567812345678",
                CardHolderName = "John Doe",
                ExpiryDate = DateTime.UtcNow.AddYears(1),
                IsActive = true,
                Balance = 1000
            }
        };

        public async Task<CardValidationResponseDto> ValidateCardAsync(CardValidationRequestDto request)
        {
            // Simulate async DB lookup
            //var card = _cards.Find(c =>
            //    c.CardNumber == request.CardNumber &&
            //    c.CardHolderName.ToLower() == request.CardHolderName.ToLower() &&
            //    c.ExpiryDate.Date == request.ExpiryDate.Date);

            var card = _cards.Find(c =>
    c.CardNumber == request.CardNumber &&
    c.CardHolderName.ToLower() == request.CardHolderName.ToLower() &&
    c.ExpiryDate.HasValue &&
    request.ExpiryDate.HasValue &&
    c.ExpiryDate.Value.Date == request.ExpiryDate.Value.Date);


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
