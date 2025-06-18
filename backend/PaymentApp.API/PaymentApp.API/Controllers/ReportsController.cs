using PaymentApp.API.DTOs;
using PaymentApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentApp.API.Repositories;

namespace PaymentApp.API.Controllers
{
    [ApiController]
    [Route("api/reports")]

    //[Route("api/[controller]/{action}")]
    //[ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public ReportsController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet("GetPayments")]
        public IActionResult GetPayments([FromQuery] PaymentReportFilterDto filter)
        {
            var result = _transactionRepository.GetFilteredAsync(filter);
            return Ok(result);
        }

        [HttpGet("GetCardBalances")]
        public IActionResult GetCardBalances([FromQuery] CardBalanceReportFilterDto filter)
        {
            var result = _transactionRepository.GetCardBalancesAsync(filter);
            return Ok(result);
        }
    }
}
