using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PostWarehouseDTOValidator : AbstractValidator<PostWarehouseDTO>
{
    public PostWarehouseDTOValidator()
    {
        RuleFor(warehouse => warehouse.Name)
            .NotEmpty().WithMessage("Name must not be empty");
    }
}