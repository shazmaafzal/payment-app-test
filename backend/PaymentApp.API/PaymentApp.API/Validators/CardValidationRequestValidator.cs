using FluentValidation;
using PaymentApp.API.DTOs;

namespace PaymentApp.API.Validators
{
    public class CardValidationRequestValidator : AbstractValidator<CardValidationRequestDto>
    {
        public CardValidationRequestValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("Card number is required")
                .Length(16).WithMessage("Card number must be 16 digits")
                .Matches(@"^\d+$").WithMessage("Card number must contain digits only");

            RuleFor(x => x.CardHolderName)
                .NotEmpty().WithMessage("Card holder name is required");

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(DateTime.UtcNow.Date).WithMessage("Card is expired");
        }
    }
}
