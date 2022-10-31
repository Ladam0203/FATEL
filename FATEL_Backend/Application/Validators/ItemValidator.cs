﻿using Application.DTOs;
using Domain;
using FluentValidation;

namespace Application.Validators;

public class ItemValidator : AbstractValidator<PostItemDTO>
{
    public ItemValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Lenght).GreaterThan(0);
        RuleFor(item => item.Width).GreaterThan(0);
        //ask alex about if statement in fluent valid. 
        //example => if unit == pieces, quantity greaterThan 0
        RuleFor(item => item.Lenght).GreaterThan(0).When(item => item.Unit == Unit.Meter);
    }

}