
using Business.Handlers.GridSettingses.Commands;
using FluentValidation;

namespace Business.Handlers.GridSettingses.ValidationRules
{

    public class CreateGridSettingsValidator : AbstractValidator<CreateGridSettingsCommand>
    {
        public CreateGridSettingsValidator()
        {
            RuleFor(x => x.Path).NotEmpty();

        }
    }
    public class UpdateGridSettingsValidator : AbstractValidator<UpdateGridSettingsCommand>
    {
        public UpdateGridSettingsValidator()
        {
            RuleFor(x => x.Path).NotEmpty();

        }
    }
}