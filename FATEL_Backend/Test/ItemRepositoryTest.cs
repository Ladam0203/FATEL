using Application;
using Application.Interfaces;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
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

    [Fact]
    public void ReadAllItems()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ItemDatabase")
            .Options;

        // Use a clean instance of the context to run the test
        using (var context = new AppDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.ItemTable.Add(new Item(){Id = 1, Name = "Item1", Quantity = 1});
            context.SaveChanges();
            
            IItemRepository repo = new ItemRepository(context);
            List<Item> items = repo.ReadAll();

            Assert.Equal("Item1", items[0].Name);
            Assert.Single(items);
        }
    }
    
    [Fact]
    public void ReadItem()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ItemDatabase")
            .Options;

        // Use a clean instance of the context to run the test
        using (var context = new AppDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.ItemTable.Add(new Item(){Id = 1, Name = "Item1", Quantity = 1});
            context.SaveChanges();
            
            IItemRepository repo = new ItemRepository(context);
            Item item = repo.Read(1);

            Assert.Equal("Item1", item.Name);
        }
    }
}