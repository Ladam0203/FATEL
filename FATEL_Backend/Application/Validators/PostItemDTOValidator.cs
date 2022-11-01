using Application.DTOs;
using Domain;
using FluentValidation;

namespace Application.Validators;

public class PostItemDTOValidator : AbstractValidator<PostItemDTO>
{
    public PostItemDTOValidator()
    {
        //TODO: Implement validation
        RuleFor(item => item.Name).NotEmpty();

        RuleFor(item => item.Unit).NotEmpty();

        //Quantity
        RuleFor(item => item.Quantity).GreaterThan(0);
        //Piece Unit
        RuleFor(item => item.Length).Empty().When(item => item.Unit == Unit.Piece);
        RuleFor(item => item.Width).Empty().When(item => item.Unit == Unit.Piece);
        //Meter Unit
        RuleFor(item => item.Length).GreaterThan(0).When(item => item.Unit == Unit.Meter);
        RuleFor(item => item.Width).Empty().When(item => item.Unit == Unit.Meter);
        //SquareMeter Unit
        RuleFor(item => item.Length).GreaterThan(0).When(item => item.Unit == Unit.SquareMeter);
        RuleFor(item => item.Width).GreaterThan(0).When(item => item.Unit == Unit.SquareMeter);
        //if note is not null then note cannot be empty
        RuleFor(item => item.Note).NotEmpty().When(item => item.Note != null);
    }
}