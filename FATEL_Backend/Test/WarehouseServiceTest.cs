using Application;
using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain;
using Moq;

namespace Test;

public class WarehouseServiceTest
{
     [Fact]
    public void CreateWarehouseService_WithNullRepository_ExpectArgumentNullException()
    {
        //Arrange
        IWarehouseService warehouseService = null;
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostWarehouseDTO, Warehouse>();
        }).CreateMapper();
        var postValidator = new PostWarehouseDTOValidator();

        //Act
        var e = Assert.Throws<ArgumentNullException>(() => warehouseService = new WarehouseService(null, postValidator, mapper));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'repository')", e.Message);
        Assert.Null(warehouseService);
    }
    
    [Fact]
    public void CreateWarehouseService_WithNullMapper_ExpectArgumentNullException()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();
        var postValidator = new PostWarehouseDTOValidator();
        IWarehouseService warehouseService = null;

        //Act
        var e = Assert.Throws<ArgumentNullException>(() => warehouseService = new WarehouseService(mockRepository.Object, postValidator, null));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'mapper')", e.Message);
        Assert.Null(warehouseService);
    }
    
    [Fact]
    public void CreateWarehouseService_WithNullPostValidator_ExpectArgumentNullException()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostWarehouseDTO, Warehouse>();
        }).CreateMapper();
        IWarehouseService warehouseService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => warehouseService = new WarehouseService(mockRepository.Object, null, mapper));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'postValidator')", e.Message);
        Assert.Null(warehouseService);
    }

    [Fact]
    public void CreateWarehouseService_WithNonNullParameters()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostWarehouseDTO, Warehouse>();
        }).CreateMapper();
        var postValidator = new PostWarehouseDTOValidator();

        //Act
        IWarehouseService warehouseService = new WarehouseService(mockRepository.Object, postValidator, mapper);
        

        //Assert
        Assert.NotNull(warehouseService);
        Assert.True(warehouseService is WarehouseService);
        //TODO: Test if the parameters were truly injected
    }

    [Fact]
    public void ReadAll()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();
        List<Warehouse> mockWarehouses = new ()
        {
            new Warehouse() {
                Id = 1,
               Name = "warehouse1",
               Diary = new List<Entry>(),
               Inventory = new List<Item>()
            },
            new Warehouse() {
                Id = 2,
                Name = "warehouse2",
                Diary = new List<Entry>(),
                Inventory = new List<Item>()
            }
        };
        mockRepository.Setup(r => r.ReadAllWarehouses()).Returns(mockWarehouses);
        
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostWarehouseDTO, Warehouse>();
        }).CreateMapper();
        var postValidator = new PostWarehouseDTOValidator();

        IWarehouseService warehouseService = new WarehouseService(mockRepository.Object, postValidator, mapper);
        //Act
        List<Warehouse> readWarehouses = warehouseService.ReadAll();

        //Assert
        Assert.NotNull(readWarehouses);
        Assert.True(readWarehouses is List<Warehouse>);
        Assert.Equal(mockWarehouses, readWarehouses);
        Assert.Equal(mockWarehouses.Count, readWarehouses.Count);
        mockRepository.Verify(r => r.ReadAllWarehouses(), Times.Once);
    }

    [Fact]
    public void CreateWarehouse_WithValidInput()
    {
        var mockRepository = new Mock<IRepositoryFacade>();
        List<Warehouse> mockWarehouses = new List<Warehouse>();
        List<Entry> mockDiary = new List<Entry>();
        List<Item> mockInventory = new List<Item>();
        Warehouse warehouse = new Warehouse() { Name = "WarehouseTest", Diary = mockDiary, Inventory = mockInventory};
        PostWarehouseDTO dto = new PostWarehouseDTO() { Name = "WarehouseTest" };

        mockRepository.Setup(r => r.CreateWarehouse(It.IsAny<Warehouse>())).Returns(() =>
        {
            mockWarehouses.Add(warehouse);
            return warehouse;
        });

        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostWarehouseDTO, Warehouse>();
        }).CreateMapper();
        var postValidator = new PostWarehouseDTOValidator();

        IWarehouseService warehouseService = new WarehouseService(mockRepository.Object, postValidator, mapper);
        
        //Act
        Warehouse result = warehouseService.Create(dto);
        
        //Assert
        Assert.NotNull(result);
        Assert.True(result is Warehouse);
        Assert.Equal(warehouse, result);
        mockRepository.Verify(r => r.CreateWarehouse(It.IsAny<Warehouse>()), Times.Once);
    }

    [Fact]
    public void DeleteWarehouse()
    {
        var mockRepository = new Mock<IRepositoryFacade>();
        int mockId = 1;
        Warehouse warehouse1 = new Warehouse
            { Id = 1, Name = "WareHouse1", Diary = new List<Entry>(), Inventory = new List<Item>() };
        Warehouse warehouse2 = new Warehouse
            { Id = 2, Name = "WareHouse2", Diary = new List<Entry>(), Inventory = new List<Item>() };
        List<Warehouse> mockWarehouses = new List<Warehouse>();
        mockWarehouses.Add(warehouse1);
        mockWarehouses.Add(warehouse2);
        mockRepository.Setup(r => r.DeleteWarehouse(mockId)).Returns(() =>
        {
            mockWarehouses.Remove(warehouse1);
            return warehouse1;
        });

        IMapper mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostWarehouseDTO, Warehouse>();
        }).CreateMapper();
        var validator = new PostWarehouseDTOValidator();

        IWarehouseService warehouseService = new WarehouseService(mockRepository.Object, validator, mapper);

        Warehouse deleteWarehouse = warehouseService.Delete(mockId);
        
        Assert.NotNull(deleteWarehouse);
        Assert.True(deleteWarehouse is Warehouse);
        Assert.Equal(warehouse1, deleteWarehouse);
        Assert.Equal(mockId, deleteWarehouse.Id);
        Assert.DoesNotContain(warehouse1, mockWarehouses);
        Assert.Contains(warehouse2, mockWarehouses);
        mockRepository.Verify(r => r.DeleteWarehouse(mockId), Times.Once);
    }
}