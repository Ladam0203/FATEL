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
        var patchValidator = new PatchItemNameDTOValidator();
        var movementValidator = new MovementValidator();

        IItemService itemService = null;

        //Act
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(null, mapper, validator, putValidator, patchValidator, movementValidator));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'repository')", e.Message);
        Assert.Null(itemService);
    }

    [Fact]
    public void CreateItemService_WithNullMapper_ExpectArgumentNullException()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        var patchValidator = new PatchItemNameDTOValidator();
        var movementValidator = new MovementValidator();

        IItemService itemService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(mockRepository.Object, null, validator, putValidator, movementValidator));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'mapper')", e.Message);
        Assert.Null(itemService);
    }

    [Fact]
    public void CreateItemService_WithNullPostValidator_ExpectArgumentNullException()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var putValidator = new PutItemDTOValidator();
        var movementValidator = new MovementValidator();

        IItemService itemService = null;

        //Act
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(mockRepository.Object, mapper, null, putValidator, movementValidator));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'postValidator')", e.Message);
        Assert.Null(itemService);
    }
    
    [Fact]
    public void CreateItemService_WithNullPutValidator_ExpectArgumentNullException()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var postValidator = new PostItemDTOValidator();
        var movementValidator = new MovementValidator();

        IItemService itemService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(mockRepository.Object, mapper, postValidator, null, movementValidator));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'putValidator')", e.Message);
        Assert.Null(itemService);
    }
    [Fact]
    public void CreateItemService_WithNullMovementValidator_ExpectArgumentNullException()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var postValidator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();

        IItemService itemService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => itemService = new ItemService(mockRepository.Object, mapper, postValidator, putValidator, null));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'movementValidator')", e.Message);
        Assert.Null(itemService);
    }

    [Fact]
    public void CreateItemService_WithNonNullParamters()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        var movementValidator = new MovementValidator();
        
        
        //Act
        IItemService itemService = new ItemService(mockRepository.Object, mapper, validator, putValidator, movementValidator);

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
        var mockRepository = new Mock<IRepositoryFacade>();
        int mockId = 1;
        Item mockItem = new()
        {
            Id = mockId
        };
        mockRepository.Setup(r => r.ReadItem(mockId)).Returns(mockItem);
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        var movementValidator = new MovementValidator();

        IItemService itemService = new ItemService(mockRepository.Object,mapper, validator, putValidator, movementValidator);
        //Act
        Item readItem = itemService.Read(mockId);

        //Assert
        Assert.NotNull(readItem);
        Assert.True(readItem is Item);
        Assert.Equal(mockItem, readItem);
        Assert.Equal(mockItem.Id, mockId);
        mockRepository.Verify(r => r.ReadItem(mockId), Times.Once);
    }
    
    //Add more coverage
    [Fact]
    public void ReadAll()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();
        List<Item> mockItems = new ()
        {
            new Item { Id = 1 },
            new Item { Id = 2 }
        };
        mockRepository.Setup(r => r.ReadAllItems()).Returns(mockItems);
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        var movementValidator = new MovementValidator();
        
        IItemService itemService = new ItemService(mockRepository.Object,mapper, validator, putValidator, movementValidator);
        //Act
        List<Item> readItems = itemService.ReadAll();

        //Assert
        Assert.NotNull(readItems);
        Assert.True(readItems is List<Item>);
        Assert.Equal(mockItems, readItems);
        Assert.Equal(mockItems.Count, readItems.Count);
        mockRepository.Verify(r => r.ReadAllItems(), Times.Once);
    }
    
    
    [Fact]
    public void Delete_WithZeroQuantity()
    {
        var mockRepository = new Mock<IRepositoryFacade>();
        int mockId = 1;
        Item item1 = new Item { Id = 1, Unit = Unit.Piece, Quantity = 0};
        Item item2 = new Item { Id = 2, Unit = Unit.Piece, Quantity = 5};
        List<Item> mockItems = new List<Item>();
        mockItems.Add(item1);
        mockItems.Add(item2);
        mockRepository.Setup(r => r.ReadItem(mockId)).Returns(item1);
        mockRepository.Setup(r => r.DeleteItem(mockId)).Returns(() =>
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
        var movementValidator = new MovementValidator();
        
        IItemService itemService = new ItemService(mockRepository.Object, mapper, validator, putValidator, movementValidator);
        
        //Act
        Item deleteItem = itemService.Delete(mockId);

        //Assert
        Assert.NotNull(deleteItem);
        Assert.True(deleteItem is Item);
        Assert.Equal(item1, deleteItem);
        Assert.Equal(mockId, deleteItem.Id);
        Assert.DoesNotContain(item1, mockItems);
        Assert.Contains(item2, mockItems);
        mockRepository.Verify(r => r.DeleteItem(mockId), Times.Once);
        mockRepository.Verify(r => r.DeleteAndRecord(It.IsAny<int>(),  It.IsAny<Entry>()), Times.Never);

    }
    
    [Fact]
    public void Delete_WithNonZeroQuantity()
    {
        var mockRepository = new Mock<IRepositoryFacade>();
        int mockId = 1;
        Item item1 = new Item { Id = 1 , Unit = Unit.Piece, Quantity = 28};
        Item item2 = new Item { Id = 2 , Unit = Unit.Piece, Quantity = 0};
        List<Item> mockItems = new List<Item>();
        mockItems.Add(item1);
        mockItems.Add(item2);
        mockRepository.Setup(r => r.ReadItem(mockId)).Returns(item1);
        mockRepository.Setup(r => r.DeleteAndRecord(mockId, It.IsAny<Entry>())).Returns(() =>
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
        var movementValidator = new MovementValidator();
        
        IItemService itemService = new ItemService(mockRepository.Object, mapper, validator, putValidator, movementValidator);
        
        //Act
        Item deleteItem = itemService.Delete(mockId);

        //Assert
        Assert.NotNull(deleteItem);
        Assert.True(deleteItem is Item);
        Assert.Equal(item1, deleteItem);
        Assert.Equal(mockId, deleteItem.Id);
        Assert.DoesNotContain(item1, mockItems);
        Assert.Contains(item2, mockItems);
        mockRepository.Verify(r => r.DeleteItem(mockId), Times.Never);
        mockRepository.Verify(r => r.DeleteAndRecord(mockId,  It.IsAny<Entry>()), Times.Once);
    }

    [Fact]
    public void Update()
    {
        int mockId = 1;
        PutItemDTO dto = new PutItemDTO() { Id = mockId, Name = "UpdatedItem", Unit = Unit.Piece};
        Item editedItem = new Item() { Id = dto.Id, Name = dto.Name, Unit = Unit.Piece };
        
        var mockRepository = new Mock<IRepositoryFacade>();
        mockRepository.Setup(r => r.UpdateItem(It.IsAny<Item>())).Returns(editedItem);
        
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
            configuration.CreateMap<PutItemDTO, Item>();
        }).CreateMapper();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        var movementValidator = new MovementValidator();
        
        IItemService itemService = new ItemService(mockRepository.Object, mapper, validator, putValidator, movementValidator);
        
        //Act
        Item updated = itemService.Update(mockId, dto);
        
        //Assert
        Assert.NotNull(updated);
        Assert.True(updated is Item);
        Assert.Equal(editedItem, updated);
        Assert.Equal(editedItem.Name, updated.Name);
        Assert.Equal(editedItem.Quantity, updated.Quantity);
        Assert.Equal(editedItem.Unit, updated.Unit);
        mockRepository.Verify(r => r.UpdateItem(It.IsAny<Item>()), Times.Once);
    }
    
    //ToDo test updateQuantity
    
    [Fact]
    public void Create_WithNonZeroQuantity()
    {
        var mockRepository = new Mock<IRepositoryFacade>();
        List<Item> mockItems = new List<Item>();
        Item item = new Item() { Name = "Item", Unit = Unit.Piece, Quantity = 1};
        PostItemDTO dto = new PostItemDTO() { Name = "Item", Unit = Unit.Piece, Quantity = 1 };

        mockRepository.Setup(r => r.CreateAndRecord(It.IsAny<Item>(), It.IsAny<Entry>())).Returns(() =>
        {
            mockItems.Add(item);
            return item;
        });
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        var movementValidator = new MovementValidator();
        
        IItemService itemService = new ItemService(mockRepository.Object,mapper, validator, putValidator, movementValidator);
        
        //Act
        Item result = itemService.Create(dto);
        
        //Assert
        Assert.NotNull(result);
        Assert.True(result is Item);
        Assert.Equal(item, result);
        mockRepository.Verify(r => r.CreateAndRecord(It.IsAny<Item>(), It.IsAny<Entry>()), Times.Once);
    }
    
    [Fact]
    public void Create_WithZeroQuantity()
    {
        var mockRepository = new Mock<IRepositoryFacade>();
        List<Item> mockItems = new List<Item>();
        Item item = new Item() { Name = "Item", Unit = Unit.Piece, Quantity = 0};
        PostItemDTO dto = new PostItemDTO() { Name = "Item", Unit = Unit.Piece, Quantity = 0 };

        mockRepository.Setup(r => r.CreateItem(It.IsAny<Item>())).Returns(() =>
        {
            mockItems.Add(item);
            return item;
        });
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostItemDTO, Item>();
        }).CreateMapper();
        var validator = new PostItemDTOValidator();
        var putValidator = new PutItemDTOValidator();
        var movementValidator = new MovementValidator();
        
        IItemService itemService = new ItemService(mockRepository.Object,mapper, validator, putValidator, movementValidator);
        
        //Act
        Item result = itemService.Create(dto);
        
        //Assert
        Assert.NotNull(result);
        Assert.True(result is Item);
        Assert.Equal(item, result);
        mockRepository.Verify(r => r.CreateItem(It.IsAny<Item>()), Times.Once);
        mockRepository.Verify(r => r.CreateAndRecord(It.IsAny<Item>(), It.IsAny<Entry>()), Times.Never);
    }
}