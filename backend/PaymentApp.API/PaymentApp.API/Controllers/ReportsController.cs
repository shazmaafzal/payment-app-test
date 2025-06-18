using PaymentApp.API.DTOs;
using PaymentApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace PaymentApp.API.Controllers
{
    [ApiController]
    [Route("api/reports")]

    //[Route("api/[controller]/{action}")]
    //[ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IPaymentTransactionStore _transactionStore;

        public ReportsController(IPaymentTransactionStore transactionStore)
        {
            _transactionStore = transactionStore;
        }

        [HttpGet("GetPayments")]
        public IActionResult GetPayments([FromQuery] PaymentReportFilterDto filter)
        {
            var result = _transactionStore.GetFilteredTransactions(filter);
            return Ok(result);
        }

        [HttpGet("GetCardBalances")]
        public IActionResult GetCardBalances([FromQuery] CardBalanceReportFilterDto filter)
        {
            var result = _transactionStore.GetCardBalances(filter);
            return Ok(result);
        }
    }
}
