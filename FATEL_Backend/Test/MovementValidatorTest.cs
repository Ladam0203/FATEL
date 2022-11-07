using Application.Validators;
using Domain;
using FluentValidation;

namespace Test;

public class MovementValidatorTest
{
    [Theory]
    //ItemId, ItemQuantity, Change, expected, expectedMessage
    //Happiest cases
    [InlineData(12, 10, 5, true, "")]
    //Happy edge cases
    [InlineData(1, 10, 10, true, "")]
    //Id sad cases
    [InlineData(0, 10, 5, false, "Item Id is in invalid range")]
    [InlineData(-1, 10, 5, false, "Item Id is in invalid range")]
    //Change sad cases
    [InlineData(1, 10, -11, false, "Change cannot make the quantity negative")]
    [InlineData(1, 10, 0, false, "Change cannot be 0")]
    public void ValidateMovementTest(int itemId, int quantity, int change, bool expectedIsValid, string expectedMessage)
    {
        //Arrange
        Item item = new()
        {
            Id = itemId,
            Quantity = quantity
        };
        Movement movement = new()
        {
            Item = item,
            Change = change
        };

        IValidator<Movement> validator = new MovementValidator();
        
        //Act
        var result = validator.Validate(movement);
        var resultIsValid = result.IsValid;
        var resultMessage = result.ToString();
        
        //Assert
        Assert.Equal(expectedMessage, resultMessage);
        Assert.Equal(expectedIsValid, resultIsValid);
    }
}