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

        IItemService itemService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(null, mapper, validator));

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
        
        IItemService itemService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(mockRepository.Object, null, validator));

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

        IItemService itemService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(mockRepository.Object, mapper, null));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'validator')", e.Message);
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
        
        //Act
        IItemService itemService = new ItemService(mockRepository.Object, mapper, validator);
        
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
        
        IItemService itemService = new ItemService(mockRepository.Object,mapper, validator);
        
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
        
        IItemService itemService = new ItemService(mockRepository.Object,mapper, validator);
        
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