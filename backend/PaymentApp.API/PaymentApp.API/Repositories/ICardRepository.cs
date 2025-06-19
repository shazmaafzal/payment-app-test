using PaymentApp.API.Models;

namespace PaymentApp.API.Repositories
{
    public interface ICardRepository
    {
        Task<Card?> GetValidCardAsync(string cardNumber, decimal? cvv, DateTime? expiryDate);
        Task UpdateAsync(Card card);
    }
}
