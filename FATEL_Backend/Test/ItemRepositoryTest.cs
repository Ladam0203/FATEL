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
        //Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ItemDatabase")
            .Options;
        
        using (var context = new AppDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.ItemTable.Add(new Item(){Id = 1, Name = "Item1", Quantity = 1});
            context.SaveChanges();

            IItemRepository repo = new ItemRepository(context);
            
            //Act
            Item item = repo.Read(1);

            //Assert
            Assert.Equal("Item1", item.Name);
        }
    }

    [Fact]
    public void DeleteItem()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ItemDatabase")
            .Options;

        using (var context = new AppDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Item item1 = new Item() { Id = 1, Name = "Item1", Quantity = 1 };
            Item item2 = new Item() { Id = 2, Name = "Item2", Quantity = 2 };
            context.ItemTable.Add(item1);
            context.ItemTable.Add(item2);
            context.SaveChanges();

            IItemRepository repo = new ItemRepository(context);

            //Act
            Item item = repo.Delete(1);
            List<Item> items = context.ItemTable.ToList();

            //Assert
            Assert.Equal("Item1", item.Name);
            Assert.Contains(item2, items);
            Assert.DoesNotContain(item1, items);
        }
    }

    [Fact]
    public void UpdateItem()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ItemDatabase")
            .Options;

        using (var context = new AppDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            int mockId = 1;
            Item item1 = new Item() { Id = mockId, Name = "Item1", Quantity = 1, Unit = Unit.Piece};
            context.ItemTable.Add(item1);
            context.SaveChanges();

            Item editedItem = new Item() { Id = mockId, Name = "EditedItem", Quantity = 5, Unit = Unit.Piece };

            IItemRepository repo = new ItemRepository(context);
            
            //Act
            Item edited = repo.Update(editedItem);
            Item readItem = context.ItemTable.Find(mockId);
            
            Assert.Equal("EditedItem", edited.Name);
            Assert.Equal("EditedItem", readItem.Name);
            Assert.Equal(edited.Quantity, readItem.Quantity);
            Assert.Equal(edited.Unit, readItem.Unit);
            Assert.Equal(edited, readItem);
        }
    }

    [Fact]
    public void UpdateItemInvalidId()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ItemDatabase")
            .Options;

        using (var context = new AppDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            int mockId = 1;
            int wrongId = 2;
            Item item1 = new Item() { Id = mockId, Name = "Item1", Quantity = 1, Unit = Unit.Piece };
            context.ItemTable.Add(item1);
            context.SaveChanges();

            Item editedItem = new Item() { Id = wrongId, Name = "EditedItem", Quantity = 5, Unit = Unit.Piece };

            IItemRepository repo = new ItemRepository(context);

            //Act + Assert
            Assert.Throws<KeyNotFoundException>(()=> repo.Update(editedItem));
        }
    }
}