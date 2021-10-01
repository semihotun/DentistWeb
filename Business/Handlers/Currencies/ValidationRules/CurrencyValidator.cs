
using Business.Handlers.Currencies.Commands;
using FluentValidation;

namespace Business.Handlers.Currencies.ValidationRules
{

    public class CreateCurrencyValidator : AbstractValidator<CreateCurrencyCommand>
    {
        public CreateCurrencyValidator()
        {
            RuleFor(x => x.Abbreviation).NotEmpty();

        }
    }
    public class UpdateCurrencyValidator : AbstractValidator<UpdateCurrencyCommand>
    {
        public UpdateCurrencyValidator()
        {
            RuleFor(x => x.Abbreviation).NotEmpty();

        }
    }
}