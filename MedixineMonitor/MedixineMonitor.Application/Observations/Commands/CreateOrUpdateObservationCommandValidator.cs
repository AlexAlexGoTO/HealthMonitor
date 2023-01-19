using FluentValidation;
using MedixineMonitor.Domain.Enums;

namespace MedixineMonitor.Application.Observations.Commands;
internal class CreateOrUpdateObservationCommandValidator : AbstractValidator<CreateOrUpdateObservationCommand>
{
    public CreateOrUpdateObservationCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty();
        RuleFor(v => v.Type)
            .IsInEnum();
        RuleFor(v => v.Value)
            .GreaterThan(0);
    }
}
