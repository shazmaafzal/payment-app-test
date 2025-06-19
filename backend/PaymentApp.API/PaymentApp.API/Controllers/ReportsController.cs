using PaymentApp.API.DTOs;
using PaymentApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace PaymentApp.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public ReportsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("GetPayments")]
        public async Task<IActionResult> GetPayments([FromQuery] PaymentReportFilterDto filter)
        {
            var result = await _transactionService.GetFilteredAsync(filter);
            return Ok(result);
        }


        [HttpGet("GetCardBalances")]
        public async Task<IActionResult> GetCardBalances([FromQuery] CardBalanceReportFilterDto filter)
        {
            var result = await _transactionService.GetCardBalancesAsync(filter);
            return Ok(result);
        }
    }
}
