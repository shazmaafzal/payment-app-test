namespace PaymentApp.API.DTOs
{
    public class PaymentReportFilterDto
    {
        public string? CardNumber { get; set; }
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
