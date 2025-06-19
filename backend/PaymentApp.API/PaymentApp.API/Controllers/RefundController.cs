using Microsoft.AspNetCore.Mvc;
using PaymentApp.API.Models;
using PaymentApp.API.Repositories;
using PaymentApp.API.Services;

namespace PaymentApp.API.Controllers
{
    [ApiController]
    [Route("api/refund")]
    public class RefundController : ControllerBase
    {
        private readonly IRefundService _refundService;

        public RefundController(IRefundService refundService)
        {
            _refundService = refundService;
        }

        [HttpPost("ProcessRefund")]
        public async Task<IActionResult> ProcessRefund([FromBody] RefundRequest request)
        {
            var result = await _refundService.ProcessRefund(request);

            return Ok(result);
        }
    }
}
