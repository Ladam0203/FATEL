using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PutWarehouseDTOValidator : AbstractValidator<PutWarehouseDTO>
{
    public PutWarehouseDTOValidator()
    {
        RuleFor(warehouse => warehouse.Name)
            .NotEmpty().WithMessage("Name must not be empty");
        RuleFor(warehouse => warehouse.Id).GreaterThan(0).WithMessage("Item Id is in invalid range");
    }
}