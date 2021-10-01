
using Business.Handlers.Patients.Commands;
using FluentValidation;

namespace Business.Handlers.Patients.ValidationRules
{

    public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientValidator()
        {
            RuleFor(x => x.IdentificationNumber).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Adress).NotEmpty();
            RuleFor(x => x.Telephone).NotEmpty();

        }
    }
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientValidator()
        {
            RuleFor(x => x.IdentificationNumber).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Adress).NotEmpty();
            RuleFor(x => x.Telephone).NotEmpty();

        }
    }
}