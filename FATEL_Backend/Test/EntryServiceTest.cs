using Application;
using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain;
using Moq;

namespace Test;

public class EntryServiceTest
{
    [Fact]
    public void CreateEntryService_WithNullRepository_ExpectArgumentNullException()
    {
        //Arrange
        IEntryService entryService = null;

        //Act
        var e = Assert.Throws<ArgumentNullException>(() => entryService = new EntryService(null));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'entryRepository')", e.Message);
        Assert.Null(entryService);
    }

    [Fact]
    public void CreateEntryService_WithNonNullParamters()
    {
        //Arrange
        var mockRepository = new Mock<IEntryRepository>();

        //Act
        IEntryService entryService = new EntryService(mockRepository.Object);

        //Assert
        Assert.NotNull(entryService);
        Assert.True(entryService is EntryService);
        //TODO: Test if the parameters were truly injected
    }

    [Fact]
    public void ReadAll()
    {
        //Arrange
        var mockRepository = new Mock<IEntryRepository>();
        List<Entry> mockEntries = new ()
        {
            new Entry {
                Id = 1,
                Timestamp = DateTime.Now,
                ItemId = 1,
                ItemName = "Item2",
                Change = 1,
                QuantityAfterChange = 1
            },
            new Entry {
                Id = 2,
                Timestamp = DateTime.Now,
                ItemId = 1,
                ItemName = "Item2",
                Change = 1,
                QuantityAfterChange = 1
            }
        };
        mockRepository.Setup(r => r.ReadAll()).Returns(mockEntries);

        IEntryService entryService = new EntryService(mockRepository.Object);
        //Act
        List<Entry> readEntries = entryService.ReadAll();

        //Assert
        Assert.NotNull(readEntries);
        Assert.True(readEntries is List<Entry>);
        Assert.Equal(mockEntries, readEntries);
        Assert.Equal(mockEntries.Count, readEntries.Count);
        mockRepository.Verify(r => r.ReadAll(), Times.Once);
    }
}