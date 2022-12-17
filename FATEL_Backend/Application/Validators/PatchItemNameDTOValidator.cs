using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PatchItemNameDTOValidator : AbstractValidator<PatchItemNameDTO>
{
    public PatchItemNameDTOValidator()
    {
        //Id is in range?
        RuleFor(item => item.Id).GreaterThan(0).WithMessage("Item Id is in invalid range");
        //Name
        RuleFor(item => item.Name)
            .NotEmpty().WithMessage("Name must not be empty");
    }
}