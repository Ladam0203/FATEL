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
            configuration.CreateMap<PutWarehouseDTO, Warehouse>();
        }).CreateMapper();
        var postValidator = new PostWarehouseDTOValidator();
        var putValidator = new PutWarehouseDTOValidator();

        //Act
        var e = Assert.Throws<ArgumentNullException>(() => warehouseService = new WarehouseService(null, postValidator, putValidator, mapper));

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
        var putValidator = new PutWarehouseDTOValidator();
        IWarehouseService warehouseService = null;

        //Act
        var e = Assert.Throws<ArgumentNullException>(() => warehouseService = new WarehouseService(mockRepository.Object, postValidator, putValidator, null));

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
            configuration.CreateMap<PutWarehouseDTO, Warehouse>();
        }).CreateMapper();
        var putValidator = new PutWarehouseDTOValidator();
        IWarehouseService warehouseService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => warehouseService = new WarehouseService(mockRepository.Object, null, putValidator, mapper));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'postValidator')", e.Message);
        Assert.Null(warehouseService);
    }
    
    public void CreateWarehouseService_WithNullPutValidator_ExpectArgumentNullException()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostWarehouseDTO, Warehouse>();
            configuration.CreateMap<PutWarehouseDTO, Warehouse>();
        }).CreateMapper();
        var postValidator = new PostWarehouseDTOValidator();
        IWarehouseService warehouseService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => warehouseService = new WarehouseService(mockRepository.Object, postValidator, null, mapper));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'putValidator')", e.Message);
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
            configuration.CreateMap<PutWarehouseDTO, Warehouse>();
        }).CreateMapper();
        var postValidator = new PostWarehouseDTOValidator();
        var putValidator = new PutWarehouseDTOValidator();

        //Act
        IWarehouseService warehouseService = new WarehouseService(mockRepository.Object, postValidator, putValidator, mapper);
        

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
            configuration.CreateMap<PutWarehouseDTO, Warehouse>();
        }).CreateMapper();
        var postValidator = new PostWarehouseDTOValidator();
        var putValidator = new PutWarehouseDTOValidator();

        IWarehouseService warehouseService = new WarehouseService(mockRepository.Object, postValidator, putValidator, mapper);
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
            configuration.CreateMap<PutWarehouseDTO, Warehouse>();
        }).CreateMapper();
        var postValidator = new PostWarehouseDTOValidator();
        var putValidator = new PutWarehouseDTOValidator();

        IWarehouseService warehouseService = new WarehouseService(mockRepository.Object, postValidator, putValidator, mapper);
        
        //Act
        Warehouse result = warehouseService.Create(dto);
        
        //Assert
        Assert.NotNull(result);
        Assert.True(result is Warehouse);
        Assert.Equal(warehouse, result);
        mockRepository.Verify(r => r.CreateWarehouse(It.IsAny<Warehouse>()), Times.Once);
    }
    
    public void Update()
    {
        int mockId = 1;
        PutWarehouseDTO dto = new PutWarehouseDTO() { Id = mockId, Name = "UpdatedWarehouse"};
        Warehouse editedwWarehouse = new Warehouse() { Id = dto.Id, Name = dto.Name};
        
        var mockRepository = new Mock<IRepositoryFacade>();
        mockRepository.Setup(r => r.UpdateWarehouse(It.IsAny<Warehouse>())).Returns(editedwWarehouse);
        
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostWarehouseDTO, Warehouse>();
            configuration.CreateMap<PutWarehouseDTO, Warehouse>();
        }).CreateMapper();
        var validator = new PostWarehouseDTOValidator();
        var putValidator = new PutWarehouseDTOValidator();

        IWarehouseService warehouseService = new WarehouseService(mockRepository.Object, validator, putValidator,  mapper);
        
        //Act
        Warehouse updated = warehouseService.Update(mockId, dto);
        
        //Assert
        Assert.NotNull(updated);
        Assert.True(updated is Warehouse);
        Assert.Equal(editedwWarehouse, updated);
        Assert.Equal(editedwWarehouse.Name, updated.Name);
        mockRepository.Verify(r => r.UpdateWarehouse(It.IsAny<Warehouse>()), Times.Once);
    }
}