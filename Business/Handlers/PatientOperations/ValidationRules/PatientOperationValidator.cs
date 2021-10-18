
using Business.Handlers.PatientOperations.Commands;
using FluentValidation;

namespace Business.Handlers.PatientOperations.ValidationRules
{

    public class CreatePatientOperationValidator : AbstractValidator<CreatePatientOperationCommand>
    {
        public CreatePatientOperationValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty();
            RuleFor(x => x.DiseaseId).NotEmpty();

        }
    }
    public class UpdatePatientOperationValidator : AbstractValidator<UpdatePatientOperationCommand>
    {
        public UpdatePatientOperationValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty();
            RuleFor(x => x.DiseaseId).NotEmpty();

        }
    }
}