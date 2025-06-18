namespace PaymentApp.API.Models
{
    public class RefundRequest
    {
        public string TransactionId { get; set; }
        public string RefundCode { get; set; }
        //RefundCode, ExpiryDate, PaymentId
    }
}
