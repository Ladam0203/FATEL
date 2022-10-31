using Application;
using Application.Interfaces;
using Infrastructure;
using Moq;

namespace Test;

public class ItemRepositoryTest
{
    [Fact]
    public void CreateItemRepository_WithNullDbContext_ExpectArgumentNullException()
    {
        //Arrange

        //Act
        IItemRepository itemRepository = null;
        var e = Assert.Throws<ArgumentNullException>(() => itemRepository = new ItemRepository(null));
        
        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'context')", e.Message);
        Assert.Null(itemRepository);
    }
    
    [Fact]
    public void CreateItemRepository_WithDbContext()
    {
        //Arrange
        var mockDbContext = new Mock<AppDbContext>();
        
        //Act
        IItemRepository itemRepository = new ItemRepository(mockDbContext.Object);
        
        //Assert
        Assert.NotNull(itemRepository);
        Assert.True(itemRepository is ItemRepository);
        //TODO: Test if the repository is truly injected
    }
}