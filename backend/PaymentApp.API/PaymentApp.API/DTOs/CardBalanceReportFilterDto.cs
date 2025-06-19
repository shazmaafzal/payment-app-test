namespace PaymentApp.API.DTOs
{
    public class CardBalanceReportFilterDto
    {
        public string? CardNumber { get; set; }
        public decimal? MinBalance { get; set; }
        public decimal? MaxBalance { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
