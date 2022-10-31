using Application;
using Application.Interfaces;
using Domain;
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

    [Fact]
    public void Read()
    {
        //Arrange
        var mockRepository = new Mock<IItemRepository>();
        int mockId = 0;
        Item mockItem = new Item(); //TODO: Use a detailed item and test with that
        mockRepository.Setup(r => r.Read(mockId)).Returns(mockItem);
        
        IItemService itemService = new ItemService(mockRepository.Object);
        
        //Act
        Item readItem = itemService.Read(mockId);

        //Assert
        Assert.NotNull(readItem);
        Assert.True(readItem is Item);
        Assert.Equal(mockItem, readItem);
        mockRepository.Verify(r => r.Read(mockId), Times.Once);
    }
    
    [Fact]
    public void ReadAll()
    {
        //Arrange
        var mockRepository = new Mock<IItemRepository>();
        List<Item> mockItems = new List<Item>(); //TODO: Use detailed items and test with that
        mockRepository.Setup(r => r.ReadAll()).Returns(mockItems);
        
        IItemService itemService = new ItemService(mockRepository.Object);
        
        //Act
        List<Item> readItems = itemService.ReadAll();

        //Assert
        Assert.NotNull(readItems);
        Assert.True(readItems is List<Item>);
        Assert.Equal(mockItems, readItems);
        mockRepository.Verify(r => r.ReadAll(), Times.Once);
    }
}