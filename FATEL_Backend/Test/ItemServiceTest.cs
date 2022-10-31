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
        int mockId = 1;
        Item mockItem = new()
        {
            Id = mockId
        };
        mockRepository.Setup(r => r.Read(mockId)).Returns(mockItem);
        
        IItemService itemService = new ItemService(mockRepository.Object);
        
        //Act
        Item readItem = itemService.Read(mockId);

        //Assert
        Assert.NotNull(readItem);
        Assert.True(readItem is Item);
        Assert.Equal(mockItem, readItem);
        Assert.Equal(mockItem.Id, mockId);
        mockRepository.Verify(r => r.Read(mockId), Times.Once);
    }
    
    [Fact]
    public void ReadAll()
    {
        //Arrange
        var mockRepository = new Mock<IItemRepository>();
        List<Item> mockItems = new ()
        {
            new Item() { Id = 1 },
            new Item() { Id = 2 }
        };
        mockRepository.Setup(r => r.ReadAll()).Returns(mockItems);
        
        IItemService itemService = new ItemService(mockRepository.Object);
        
        //Act
        List<Item> readItems = itemService.ReadAll();

        //Assert
        Assert.NotNull(readItems);
        Assert.True(readItems is List<Item>);
        Assert.Equal(mockItems, readItems);
        Assert.Equal(mockItems.Count, readItems.Count);
        mockRepository.Verify(r => r.ReadAll(), Times.Once);
    }
}