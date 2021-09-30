
using Business.Handlers.DoctorTypes.Commands;
using FluentValidation;

namespace Business.Handlers.DoctorTypes.ValidationRules
{

    public class CreateDoctorTypeValidator : AbstractValidator<CreateDoctorTypeCommand>
    {
        public CreateDoctorTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

        }
    }
    public class UpdateDoctorTypeValidator : AbstractValidator<UpdateDoctorTypeCommand>
    {
        public UpdateDoctorTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

        }
    }
}