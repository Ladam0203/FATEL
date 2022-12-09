using Application.Interfaces;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test;

public class WarehouseRepositoryTest
{
    [Fact]
    public void CreateWarehouseRepository_WithNullDbContext_ExpectArgumentException()
    {
        //Arrange
        //Act
        IWarehouseRepository warehouseRepository = null;

        var e = Assert.Throws<ArgumentNullException>(() => warehouseRepository = new WarehouseRepository(null));
        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'context')", e.Message);
        Assert.Null(warehouseRepository);
    }

    [Fact]
    public void CreateWarehouseRepository_WithDbContext()
    {
        //Arrange
        var mockDbContext = new Mock<AppDbContext>();
        //Act
        IWarehouseRepository warehouseRepository = new WarehouseRepository(mockDbContext.Object);

        //Assert
        Assert.NotNull(warehouseRepository);
        Assert.True(warehouseRepository is WarehouseRepository);
    }

    [Fact]
    public void ReadAllWarehouses()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "WarehouseDatabase")
            .Options;
        
        using (var context = new AppDbContext(options))
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.WarehouseTable.Add(new Warehouse()
                { Id = 1, Name = "Warehouse" });
            context.SaveChanges();
            
            //Act
            IWarehouseRepository repository = new WarehouseRepository(context);
            List<Warehouse> warehouses = repository.ReadAll();
            
            //Assert
            Assert.Equal("Warehouse", warehouses[0].Name);
            Assert.Single(warehouses);
        };

    }
}