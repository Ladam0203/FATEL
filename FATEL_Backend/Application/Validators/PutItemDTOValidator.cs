using Application.DTOs;
using Domain;
using FluentValidation;

namespace Application.Validators;

public class PutItemDTOValidator : AbstractValidator<PutItemDTO>
{
    public PutItemDTOValidator()
    {
        //Id is in range?
        RuleFor(item => item.Id).GreaterThan(0).WithMessage("Item Id is in invalid range");
        //Name
        RuleFor(item => item.Name)
            .NotEmpty().WithMessage("Name must not be empty");
        //Unit
        RuleFor(item => item.Unit)
            .NotNull()
            .IsInEnum().WithMessage("Unit must not be a valid enum value");

        //Piece Unit
        RuleFor(item => item.Length)
            .Null().When(item => item.Unit == Unit.Piece).WithMessage("Length must be null if the unit is Piece");
        RuleFor(item => item.Width)
            .Null().When(item => item.Unit == Unit.Piece).WithMessage("Width must be null if the unit is Piece");
        //Meter Unit
        RuleFor(item => item.Length)
            .NotNull().When(item => item.Unit == Unit.Meter).WithMessage("Length must not be null if the unit is Meter");
        RuleFor(item => item.Length)
            .GreaterThan(0).When(item => item.Unit == Unit.Meter).WithMessage("Length has to be greater than 0 if the unit is Meter");
        RuleFor(item => item.Width)
            .Null().When(item => item.Unit == Unit.Meter).WithMessage("Width must be null if the unit is Meter");
        //SquareMeter Unit
        RuleFor(item => item.Length)
            .NotNull().When(item => item.Unit == Unit.SquareMeter).WithMessage("Length must not be null if the unit is SquareMeter");
        RuleFor(item => item.Length)
            .GreaterThan(0).When(item => item.Unit == Unit.SquareMeter).WithMessage("Length has to be greater than 0 if the unit is SquareMeter");
        RuleFor(item => item.Width)
            .NotNull().When(item => item.Unit == Unit.SquareMeter).WithMessage("Width must not be null if the unit is SquareMeter");
        RuleFor(item => item.Width)
            .GreaterThan(0).When(item => item.Unit == Unit.SquareMeter).WithMessage("Width has to be greater than 0 if the unit is SquareMeter");
        //Note
        RuleFor(item => item.Note)
            .NotEmpty().When(item => item.Note != null).WithMessage("Note must not be empty");
    }
}