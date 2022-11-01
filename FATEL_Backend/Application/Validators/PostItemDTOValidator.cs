using Application.DTOs;
using Domain;
using FluentValidation;

namespace Application.Validators;

public class PostItemDTOValidator : AbstractValidator<PostItemDTO>
{
    public PostItemDTOValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        //ask alex about if statement in fluent valid. 
        //check lenght if unit == meter
        RuleFor(item => item.Lenght).GreaterThan(0).When(item => item.Unit == Unit.Meter);
        //check quantity if unit == piece
        RuleFor(item => item.Quantity).NotEmpty().When(item => item.Unit == Unit.Piece);
        //check lenght and width if unit == square meter
        RuleFor(item => item.Lenght).GreaterThan(0).When(item => item.Unit == Unit.SquareMeter);
        RuleFor(item => item.Width).GreaterThan(0).When(item => item.Unit == Unit.SquareMeter);
        //if note is not null then note cannot be empty
        RuleFor(item => item.Note).NotEmpty().When(item => item.Note != null);
    }
}