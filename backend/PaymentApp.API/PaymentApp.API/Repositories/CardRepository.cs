using PaymentApp.API.Models;

namespace PaymentApp.API.Repositories
{
    public class CardRepository : ICardRepository
    {
        public Task<Card?> GetValidCardAsync(string cardNumber, string cardHolderName, DateTime? expiryDate)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Card card)
        {
            throw new NotImplementedException();
        }
    }
}
