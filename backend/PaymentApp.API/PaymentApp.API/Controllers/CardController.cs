using Microsoft.AspNetCore.Mvc;
using PaymentApp.API.DTOs;
using PaymentApp.API.Services;

namespace PaymentApp.API.Controllers
{
    [ApiController]
    [Route("api/card")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("ValidateCard")]
        public async Task<IActionResult> ValidateCard([FromBody] CardValidationRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cardService.ValidateCardAsync(request);

            return Ok(result);
        }
    }
}
