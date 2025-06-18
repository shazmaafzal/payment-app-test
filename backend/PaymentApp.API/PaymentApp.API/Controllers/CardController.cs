using Microsoft.AspNetCore.Mvc;
using PaymentApp.API.DTOs;
using PaymentApp.API.Services;

namespace PaymentApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateCard([FromBody] CardValidationRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cardService.ValidateCardAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
