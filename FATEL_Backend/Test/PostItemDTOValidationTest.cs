using System.Runtime.Intrinsics;
using Application.DTOs;
using Application.Validators;
using Domain;

namespace Test;

public class PostItemDTOValidationTest
{
    [Fact]
    public void ValidatePostItemDTO_WithAllValidFields()
    {
        //Arrange
        PostItemDTO postItemDTO = new PostItemDTO() { Name = "Plank", Lenght = 2, Note = "This is a plank", Unit = Unit.Meter };
        var validator = new PostItemDTOValidator();
        //Act
        var e = validator.Validate(postItemDTO);
        //Assert
        Assert.True(e.IsValid);
    }
    
    
}