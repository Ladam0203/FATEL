using Application.Interfaces;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test;

public class EntryRepositoryTest
{
    [Fact]
    public void CreateEntryRepository_WithNullDbContext_ExpectArgumentNullException()
    {
        //Arrange

        //Act
        IEntryRepository entryRepository = null;
        var e = Assert.Throws<ArgumentNullException>(() => entryRepository = new EntryRepository(null));
        
        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'context')", e.Message);
        Assert.Null(entryRepository);
    }
        
    [Fact]
    public void CreateEntryRepository_WithDbContext()
    {
        //Arrange
        var mockDbContext = new Mock<AppDbContext>();
        
        //Act
        IEntryRepository entryRepository = new EntryRepository(mockDbContext.Object);
        
        //Assert
        Assert.NotNull(entryRepository);
        Assert.True(entryRepository is EntryRepository);
        //TODO: Test if the repository is truly injected
    }

    [Fact]
    public void CreateEntry()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "EntryDatabase")
            .Options;

        using (var context = new AppDbContext(options))
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            Entry entry1 = new Entry()
            {
                Timestamp = DateTime.Now,
                ItemId = 1,
                ItemName = "Item",
                Change = 1,
                QuantityAfterChange = 1
            };
            
            IEntryRepository repo = new EntryRepository(context);
            
            //Act
            Entry result = repo.Create(entry1);
            Entry foundResult = context.EntryTable.Find(result.Id);
            
            
            //Assert
            Assert.Equal(entry1, result);
            Assert.Equal(entry1, foundResult);
        }
    }
    
    [Fact]
    public void ReadAllEntries()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "EntryDatabase")
            .Options;

        // Use a clean instance of the context to run the test
        using (var context = new AppDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            Entry entry1 = new Entry()
            {
                Id = 1,
                Timestamp = DateTime.Now,
                ItemId = 1,
                ItemName = "Item",
                Change = 1,
                QuantityAfterChange = 1
            };
            Entry entry2 = new Entry()
            {
                Id = 2,
                Timestamp = DateTime.Now,
                ItemId = 2,
                ItemName = "Item2",
                Change = 1,
                QuantityAfterChange = 2
            };
            context.EntryTable.Add(entry1);
            context.EntryTable.Add(entry2);
            context.SaveChanges();
            
            IEntryRepository repo = new EntryRepository(context);
            List<Entry> entries = repo.ReadAll();

            Assert.Equal(entry1, entries[0]);
            Assert.Equal(entry2, entries[1]);
            Assert.Equal(2, entries.Count);
        }
    }
}