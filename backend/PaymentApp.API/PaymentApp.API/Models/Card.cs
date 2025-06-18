namespace PaymentApp.API.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }

        //CVV is remaining
    }
}
