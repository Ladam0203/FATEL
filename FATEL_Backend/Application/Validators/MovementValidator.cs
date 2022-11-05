using Domain;
using FluentValidation;

namespace Application.Validators;

public class MovementValidator : AbstractValidator<Movement>
{
    public MovementValidator()
    {
        RuleFor(movement => movement.Item.Id).GreaterThan(0).WithMessage("Item Id is in invalid range");
        RuleFor(movement => movement.Change).NotEqual(0).WithMessage("Change cannot be 0");
    }
}