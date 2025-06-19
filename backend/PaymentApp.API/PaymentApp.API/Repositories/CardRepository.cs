using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PaymentApp.API.Data;
using PaymentApp.API.Models;

namespace PaymentApp.API.Repositories
{
    public class CardRepository : ICardRepository
    {

        private readonly AppDbContext _context;

        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Card?> GetValidCardAsync(string cardNumber, decimal? cvv, DateTime? expiryDate)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || cvv <= 0 || !expiryDate.HasValue)
                return null;

            return await _context.Cards
                .FirstOrDefaultAsync(c =>
                    c.CardNumber == cardNumber &&
                    //c.CardHolderName.ToLower() == request.CardHolderName.ToLower() &&
                    c.CVV == cvv &&
                    c.ExpiryDate.HasValue &&
                    c.ExpiryDate.Value.Year == expiryDate.Value.Year &&
                    c.ExpiryDate.Value.Month == expiryDate.Value.Month);
        }


        public async Task<Card?> GetByCardNumberAsync(string cardNumber)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
        }

        public async Task UpdateAsync(Card card)
        {
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Card card)
        {
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
        }
    }
}
