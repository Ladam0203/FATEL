using Application.DTOs;
using Application.Validators;
using Domain;

namespace Test;

public class PutItemDTOValidatorTest
{
    [Theory]
    //Happy cases
    [InlineData(10, "Name 1", 1, null, Unit.Meter, "Note", true, "")]
    [InlineData(11, "Name 2", 1, 2, Unit.SquareMeter, null, true, "")]
    [InlineData(12, "Name 3", null, null, Unit.Piece, "Note 1", true, "")]
    //Id sad and edge cases
    [InlineData(-1, "Name", 1, 2, Unit.SquareMeter, "Note", false, "Item Id is in invalid range")]
    [InlineData(0, "Name", 1, 2, Unit.SquareMeter, "Note", false, "Item Id is in invalid range")]
    //Name sad cases
    [InlineData(1, "", 1, 2, Unit.SquareMeter, "Note", false, "Name must not be empty")]
    [InlineData(1, null, 1, 2, Unit.SquareMeter, "Note", false, "Name must not be empty")]
    //Note sad cases
    [InlineData(10, "Name 1", 1, null, Unit.Meter, "", false, "Note must not be empty")]
    //Meter sad cases
    [InlineData(1, "Name", null, 2, Unit.Meter, "Note", false, "Length must not be null if the unit is Meter\r\nWidth must be null if the unit is Meter")]
    [InlineData(1, "Name", null, null, Unit.Meter, "Note", false, "Length must not be null if the unit is Meter")]
    //SquareMeter sad cases
    [InlineData(1, "Name", null, null, Unit.SquareMeter, "Note", false, "Length must not be null if the unit is SquareMeter\r\nWidth must not be null if the unit is SquareMeter")]
    [InlineData(1, "Name", 1, null, Unit.SquareMeter, "Note", false, "Width must not be null if the unit is SquareMeter")]
    [InlineData(1, "Name", null, 1, Unit.SquareMeter, "Note", false, "Length must not be null if the unit is SquareMeter")]
    //Piece sad cases
    [InlineData(1, "Name", null, 1, Unit.Piece, "Note", false, "Width must be null if the unit is Piece")]
    [InlineData(1, "Name", 1, null, Unit.Piece, "Note", false, "Length must be null if the unit is Piece")]
    public void ValidatePutItemDTO(int id, string name, float? length, float? width, Unit unit, string note, bool expected, string expectedMessage)
    {
        //Arrange
        PutItemDTO putItemDTO = new()
        {
            Id = id,
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