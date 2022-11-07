using Application.DTOs;
using Application.Validators;
using Domain;

namespace Test;

public class PutItemDTOValidatorTest
{
    [Theory]
    //Happy cases
    [InlineData("Name", 1, 2, Unit.SquareMeter, "Note", true, "")]
    public void ValidatePutItemDTO(string name, float? length, float? width, Unit unit, string note, bool expected, string expectedMessage)
    {
        //Arrange
        PutItemDTO putItemDTO = new()
        {
            Name = name,
            Length = length,
            Width = width,
            Unit = unit,
            Note = note
        };
        
        var validator = new PutItemDTOValidator();
        //Act
        var result = validator.Validate(putItemDTO);
        var resultIsValid = result.IsValid;
        var resultMessage = result.ToString();
        //Assert
        Assert.Equal(expectedMessage, resultMessage);
        Assert.Equal(expected, resultIsValid);
    }
}