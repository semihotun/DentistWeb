
using Business.Handlers.AppointmentTimes.Commands;
using FluentValidation;

namespace Business.Handlers.AppointmentTimes.ValidationRules
{

    public class CreateAppointmentTimeValidator : AbstractValidator<CreateAppointmentTimeCommand>
    {
        public CreateAppointmentTimeValidator()
        {
            RuleFor(x => x.Hour).NotEmpty();
            RuleFor(x => x.Minutes).NotEmpty();

        }
    }
    public class UpdateAppointmentTimeValidator : AbstractValidator<UpdateAppointmentTimeCommand>
    {
        public UpdateAppointmentTimeValidator()
        {
            RuleFor(x => x.Hour).NotEmpty();
            RuleFor(x => x.Minutes).NotEmpty();

        }
    }
}