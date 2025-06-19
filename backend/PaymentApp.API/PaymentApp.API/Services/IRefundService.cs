using PaymentApp.API.DTOs;
using PaymentApp.API.Models;

namespace PaymentApp.API.Services
{
    public interface IRefundService
    {
        Task<ResponseDto> ProcessRefund(RefundRequest request);
    }
}
