using Microsoft.AspNetCore.Mvc;
using PaymentApp.API.DTOs;
using PaymentApp.API.Services;

namespace PaymentApp.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IPaymentTransactionStore _transactionStore;

        public ReportsController(IPaymentTransactionStore transactionStore)
        {
            _transactionStore = transactionStore;
        }

        [HttpGet("payments")]
        public IActionResult GetPayments([FromQuery] PaymentReportFilterDto filter)
        {
            var result = _transactionStore.GetFilteredTransactions(filter);
            return Ok(result);
        }

        [HttpGet("card-balances")]
        public IActionResult GetCardBalances([FromQuery] CardBalanceReportFilterDto filter)
        {
            var result = _transactionStore.GetCardBalances(filter);
            return Ok(result);
        }
    }
}
