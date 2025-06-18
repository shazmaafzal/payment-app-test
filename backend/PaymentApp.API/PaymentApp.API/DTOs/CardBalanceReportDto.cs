namespace PaymentApp.API.DTOs
{
    public class CardBalanceReportDto
    {
        public string CardNumber { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal RemainingBalance { get; set; }
    }
}
