using System.Runtime.Intrinsics;
using Application.DTOs;
using Application.Validators;
using Domain;
using FluentValidation.Validators;

namespace Test;

public class PostItemDTOValidationTest
{
    [Theory]
    //Testing null/empty values at inappropriate places, not regarding length and width7
    //Name
    [InlineData(null, null, null, Unit.Piece, 2, "Valid note", false, "Name must not be empty")]
    [InlineData("", null, null, Unit.Piece, 2, "Valid note", false, "Name must not be empty")]
    //Unit
    //Unit cannot be null, programatically TODO: Ask Bent
    //Note is okay to be null, but not empty
    [InlineData("Valid name", null, null, Unit.Piece, 2, null, true, "")]
    [InlineData("Valid name", null, null, Unit.Piece, 2, "", false, "Note must not be empty")]
    //Testing invalid numbers
    [InlineData("Valid name", null, null, Unit.Piece, -2, "Valid note", false, "Quantity has to be greater than or equal to 0")]
    [InlineData("Valid name", -2, null, Unit.Meter, 2, "Valid note", false, "Length has to be greater than 0 if the unit is Meter")]
    [InlineData("Valid name", 2, -3, Unit.SquareMeter, 2, "Valid note", false, "Width has to be greater than 0 if the unit is SquareMeter")]
    //Testing the "width/lenght based on unit problem" - Happy cases
    [InlineData("Valid name", null, null, Unit.Piece, 2, "Valid note", true, "")]
    [InlineData("Valid name", 2, null, Unit.Meter, 2, "Valid note", true, "")]
    [InlineData("Valid name", 2, 3, Unit.SquareMeter, 2, "Valid note", true, "")]
    //Testing the "width/lenght based on unit problem" - Sad cases, null values
    [InlineData("Valid name", 1, null, Unit.Piece, 2, "Valid note", false, "Length must be null if the unit is Piece")]
    [InlineData("Valid name", 1, 3, Unit.Meter, 2, "Valid note", false, "Width must be null if the unit is Meter")]
    [InlineData("Valid name", null, 3, Unit.SquareMeter, 2, "Valid note", false, "Length must not be null if the unit is SquareMeter")]
    //Testing the "width/lenght based on unit problem" - Sad cases, invalid values
    [InlineData("Valid name", 2, -3, Unit.SquareMeter, 2, "Valid note", false, "Width has to be greater than 0 if the unit is SquareMeter")]
    public void ValidatePostItemDTO(string name, float? length, float? width, Unit unit, int quantity, string note, bool expected, string expectedMessage)
    {
        //Arrange
        PostItemDTO postItemDTO = new()
        {
            Name = name,
            Length = length,
            Width = width,
            Unit = unit,
            Quantity = quantity,
            Note = note
        };
        
        var validator = new PostItemDTOValidator();
        //Act
        var result = validator.Validate(postItemDTO);
        var resultIsValid = result.IsValid;
        var resultMessage = result.ToString();
        //Assert
        Assert.Equal(expectedMessage, resultMessage);
        Assert.Equal(expected, resultIsValid);
    }
}