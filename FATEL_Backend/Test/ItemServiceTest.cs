using Application;
using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain;
using Moq;

namespace Test;

public class ItemServiceTest
{
    //TODO: Extend tests for checking if the mapper and the validators are not null
    [Fact]
    public void CreateItemService_WithNullRepository_ExpectArgumentNullException()
    {
        //Arrange
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();

        IItemService itemService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(null, mapper, validator, putValidator));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'itemRepository')", e.Message);
        Assert.Null(itemService);
    }

    [Fact]
    public void CreateItemService_WithNullMapper_ExpectArgumentNullException()
    {
        //Arrange
        var mockRepository = new Mock<IItemRepository>();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        
        IItemService itemService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(mockRepository.Object, null, validator, putValidator));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'mapper')", e.Message);
        Assert.Null(itemService);
    }

    [Fact]
    public void CreateItemService_WithNullValidator_ExpectArgumentNullException()
    {
        //Arrange
        var mockRepository = new Mock<IItemRepository>();
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var putValidator = new PutItemDTOValidator();

        IItemService itemService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(mockRepository.Object, mapper, null, putValidator));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'postValidator')", e.Message);
        Assert.Null(itemService);
    }
    
    [Fact]
    public void CreateItemService_WithNullPutValidator_ExpectArgumentNullException()
    {
        //Arrange
        var mockRepository = new Mock<IItemRepository>();
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var postValidator = new PostItemDTOValidator();

        IItemService itemService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(mockRepository.Object, mapper, postValidator, null));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'putValidator')", e.Message);
        Assert.Null(itemService);
    }

    [Fact]
    public void CreateItemService_WithNonNullParamters()
    {
        //Arrange
        var mockRepository = new Mock<IItemRepository>();
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        
        //Act
        IItemService itemService = new ItemService(mockRepository.Object, mapper, validator, putValidator);
        
        //Assert
        Assert.NotNull(itemService);
        Assert.True(itemService is ItemService);
        //TODO: Test if the parameters were truly injected
    }

    //TODO: Add more coverage
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
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        
        IItemService itemService = new ItemService(mockRepository.Object,mapper, validator, putValidator);
        
        //Act
        Item readItem = itemService.Read(mockId);

        //Assert
        Assert.NotNull(readItem);
        Assert.True(readItem is Item);
        Assert.Equal(mockItem, readItem);
        Assert.Equal(mockItem.Id, mockId);
        mockRepository.Verify(r => r.Read(mockId), Times.Once);
    }
    
    //Add more coverage
    [Fact]
    public void ReadAll()
    {
        //Arrange
        var mockRepository = new Mock<IItemRepository>();
        List<Item> mockItems = new ()
        {
            new Item { Id = 1 },
            new Item { Id = 2 }
        };
        mockRepository.Setup(r => r.ReadAll()).Returns(mockItems);
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        
        IItemService itemService = new ItemService(mockRepository.Object,mapper, validator, putValidator);
        
        //Act
        List<Item> readItems = itemService.ReadAll();

        //Assert
        Assert.NotNull(readItems);
        Assert.True(readItems is List<Item>);
        Assert.Equal(mockItems, readItems);
        Assert.Equal(mockItems.Count, readItems.Count);
        mockRepository.Verify(r => r.ReadAll(), Times.Once);
    }

    [Fact]
    public void Delete()
    {
        var mockRepository = new Mock<IItemRepository>();
        int mockId = 1;
        Item item1 = new Item { Id = 1 };
        Item item2 = new Item { Id = 2 };
        List<Item> mockItems = new List<Item>();
        mockItems.Add(item1);
        mockItems.Add(item2);
        mockRepository.Setup(r => r.Delete(mockId)).Returns(() =>
        {
            mockItems.Remove(item1);
            return item1;
        });
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        
        IItemService itemService = new ItemService(mockRepository.Object, mapper, validator, putValidator);
        
        //Act
        Item deleteItem = itemService.Delete(mockId);

        //Assert
        Assert.NotNull(deleteItem);
        Assert.True(deleteItem is Item);
        Assert.Equal(item1, deleteItem);
        Assert.Equal(mockId, deleteItem.Id);
        Assert.DoesNotContain(item1, mockItems);
        Assert.Contains(item2, mockItems);
        mockRepository.Verify(r => r.Delete(mockId), Times.Once);
    }

    [Fact]
    public void Update()
    {
        int mockId = 1;
        PutItemDTO dto = new PutItemDTO() { Id = mockId, Name = "UpdatedItem", Quantity = 5, Unit = Unit.Piece};
        Item editedItem = new Item() { Id = dto.Id, Name = dto.Name, Quantity = dto.Quantity, Unit = Unit.Piece };
        
        var mockRepository = new Mock<IItemRepository>();
        mockRepository.Setup(r => r.Update(It.IsAny<Item>())).Returns(editedItem);
        
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
            configuration.CreateMap<PutItemDTO, Item>();
        }).CreateMapper();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        
        IItemService itemService = new ItemService(mockRepository.Object, mapper, validator, putValidator);
        
        //Act
        Item updated = itemService.Update(mockId, dto);
        
        //Assert
        Assert.NotNull(updated);
        Assert.True(updated is Item);
        Assert.Equal(editedItem, updated);
        Assert.Equal(editedItem.Name, updated.Name);
        Assert.Equal(editedItem.Quantity, updated.Quantity);
        Assert.Equal(editedItem.Unit, updated.Unit);
        mockRepository.Verify(r => r.Update(It.IsAny<Item>()), Times.Once);
    }
}