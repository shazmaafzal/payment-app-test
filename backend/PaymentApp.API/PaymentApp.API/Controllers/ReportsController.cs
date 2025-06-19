using PaymentApp.API.DTOs;
using PaymentApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("GetPaymentSummary")]
        public async Task<IActionResult> GetPaymentSummary()
        {
            var summary = await _transactionService.GetPaymentSummaryAsync();
            return Ok(summary);
        }

        [HttpGet("GetPaymentsTrend")]
        public async Task<IActionResult> GetPaymentsTrend([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var trend = await _transactionService.GetPaymentsTrendAsync(startDate, endDate);
            return Ok(trend);
        }

        [HttpGet("GetPaymentStatusPie")]
        public async Task<IActionResult> GetPaymentStatusPie()
        {
            var data = await _transactionService.GetPaymentStatusPieAsync();
            return Ok(data);
        }

    }
}
