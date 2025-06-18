using Microsoft.AspNetCore.Mvc;
using PaymentApp.API.DTOs;
using PaymentApp.API.Services;

namespace PaymentApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> Process([FromBody] PaymentRequestDto request)
        {
            var result = await _paymentService.ProcessPaymentAsync(request);

            if (string.IsNullOrEmpty(result.TransactionId))
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
