using Application;
using Application.Interfaces;
using Moq;

namespace Test;

public class ItemServiceTest
{
    [Fact]
    public void CreateItemService_WithNullRepository_ExpectArgumentNullException()
    {
        //Arrange

        //Act
        IItemService itemService = null;
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(null));
        
        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'itemRepository')", e.Message);
        Assert.Null(itemService);
    }
    
    [Fact]
    public void CreateItemService_WithRepository()
    {
        //Arrange
        var mockRepository = new Mock<IItemRepository>();
        
        //Act
        IItemService itemService = new ItemService(mockRepository.Object);
        
        //Assert
        Assert.NotNull(itemService);
        Assert.True(itemService is ItemService);
        //TODO: Test if the repository is truly injected
    }
}