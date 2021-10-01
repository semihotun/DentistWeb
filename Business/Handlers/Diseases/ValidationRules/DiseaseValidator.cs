
using Business.Handlers.Diseases.Commands;
using FluentValidation;

namespace Business.Handlers.Diseases.ValidationRules
{

    public class CreateDiseaseValidator : AbstractValidator<CreateDiseaseCommand>
    {
        public CreateDiseaseValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.CurrencyId).NotEmpty();

        }
    }
    public class UpdateDiseaseValidator : AbstractValidator<UpdateDiseaseCommand>
    {
        public UpdateDiseaseValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.CurrencyId).NotEmpty();

        }
    }
}