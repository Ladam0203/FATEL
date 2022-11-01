using System.Runtime.Intrinsics;
using Application.DTOs;
using Application.Validators;
using Domain;
using FluentValidation.Validators;

namespace Test;

public class PostItemDTOValidationTest
{
    [Fact]
    public void ValidatePostItemDTO_WithAllValidFields()
    {
        //Arrange
        PostItemDTO postItemDTO = new PostItemDTO() { Name = "Plank", Length = 2, Note = "This is a plank", Unit = Unit.Meter };
        var validator = new PostItemDTOValidator();
        //Act
        var e = validator.Validate(postItemDTO);
        //Assert
        Assert.True(e.IsValid);
    }
    
    [Theory]
    //Testing null/empty values at inappropriate places
    [InlineData(null, 2, 2, Unit.SquareMeter, 2, "This is a plank", false)]
    [InlineData("", 2, 2, Unit.SquareMeter, 2, "This is a plank", false)]
    [InlineData("Plank", 2, 2, null, 2, "This is a plank", false)]
    [InlineData("Plank", 2, 2, Unit.SquareMeter, null, "This is a plank", false)]
    //Note is okay to be null, but not empty
    [InlineData("plank", 2, 2, Unit.SquareMeter, 2, "", false)]
    [InlineData("plank", 2, 2, Unit.SquareMeter, 2, null, true)]
    //Testing invalid numbers
    [InlineData("Plank", -2, 2, Unit.SquareMeter, 2, "This is a plank", false)]
    [InlineData("Plank", 2, -2, Unit.SquareMeter, 2, "This is a plank", false)]
    [InlineData("Plank", 2, 2, Unit.SquareMeter, -2, "This is a plank", false)]
    [InlineData("Plank", -2, -2, Unit.SquareMeter, -2, "This is a plank", false)]
    //Testing the "width/lenght based on unit problem" - Happy cases
    [InlineData("Plank", null, 2, Unit.Meter, 2, "This is a plank", true)]
    [InlineData("Plank", 2, 2, Unit.SquareMeter, 2, "This is a plank", true)]
    [InlineData("Plank", null, null, Unit.Piece, 2, "This is a plank", true)]
    //Testing the "width/lenght based on unit problem" - Sad cases
    [InlineData("Plank", 2, null, Unit.Meter, 2, "This is a plank", false)]
    [InlineData("Plank", 0, 2, Unit.SquareMeter, 2, "This is a plank", false)]
    [InlineData("Plank", 2, 2, Unit.Piece, 2, "This is a plank", false)]
    public void ValidatePostItemDTO(string name, float width, float length, Unit unit, int quantity, string note, bool expected)
    {
        //Arrange
        PostItemDTO postItemDTO = new()
        {
            Name = name,
            Width = width,
            Length = length,
            Unit = unit,
            Quantity = quantity,
            Note = note
        };
        
        var validator = new PostItemDTOValidator();
        //Act
        var result = validator.Validate(postItemDTO).IsValid;
        //Assert
        Assert.Equal(expected, result);
    }
}