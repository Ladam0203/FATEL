using Application;
using Application.Interfaces;
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

        //Act
        var e = Assert.Throws<ArgumentNullException>(() => warehouseService = new WarehouseService(null));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'repository')", e.Message);
        Assert.Null(warehouseService);
    }

    [Fact]
    public void CreateWarehouseService_WithNonNullParameters()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();

        //Act
        IWarehouseService warehouseService = new WarehouseService(mockRepository.Object);
        

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

        IWarehouseService warehouseService = new WarehouseService(mockRepository.Object);
        //Act
        List<Warehouse> readWarehouses = warehouseService.ReadAll();

        //Assert
        Assert.NotNull(readWarehouses);
        Assert.True(readWarehouses is List<Warehouse>);
        Assert.Equal(mockWarehouses, readWarehouses);
        Assert.Equal(mockWarehouses.Count, readWarehouses.Count);
        mockRepository.Verify(r => r.ReadAllWarehouses(), Times.Once);
    }
}