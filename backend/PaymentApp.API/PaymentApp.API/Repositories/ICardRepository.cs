using PaymentApp.API.Models;

namespace PaymentApp.API.Repositories
{
    public interface ICardRepository
    {
        Task<Card?> GetValidCardAsync(string cardNumber, string cardHolderName, DateTime? expiryDate);
        Task UpdateAsync(Card card);
    }
}
