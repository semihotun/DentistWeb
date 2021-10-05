
using Business.Handlers.Doctors.Commands;
using FluentValidation;

namespace Business.Handlers.Doctors.ValidationRules
{

    public class CreateDoctorValidator : AbstractValidator<CreateDoctorCommand>
    {
        public CreateDoctorValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Adress).NotEmpty();
            RuleFor(x => x.Telephone).NotEmpty();
            RuleFor(x => x.DoctorTypeId).NotEmpty();
        }
    }
    public class UpdateDoctorValidator : AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Adress).NotEmpty();
            RuleFor(x => x.Telephone).NotEmpty();
            RuleFor(x => x.DoctorTypeId).NotEmpty();
        }
    }
}