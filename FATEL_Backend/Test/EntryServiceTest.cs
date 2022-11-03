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
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostEntryDTO, Entry>();
        }).CreateMapper();
        var validator = new PostEntryDTOValidator();
        

        IEntryService entryService = null;

        //Act
        var e = Assert.Throws<ArgumentNullException>(() => entryService = new EntryService(null, mapper, validator));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'entryRepository')", e.Message);
        Assert.Null(entryService);
    }
    
    [Fact]
    public void CreateEntryService_WithNullMapper_ExpectArgumentNullException()
    {
        //Arrange
        var mockRepository = new Mock<IEntryRepository>();
        var validator = new PostEntryDTOValidator();

        IEntryService entryService = null;
        
        //Act
        var e = Assert.Throws<ArgumentNullException>(() => entryService = new EntryService(mockRepository.Object, null, validator));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'mapper')", e.Message);
        Assert.Null(entryService);
    }
    
    [Fact]
    public void CreateEntryService_WithNullPostValidator_ExpectArgumentNullException()
    {
        //Arrange
        var mockRepository = new Mock<IEntryRepository>();
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostEntryDTO, Entry>();
        }).CreateMapper();

        IEntryService entryService = null;

        //Act
        var e = Assert.Throws<ArgumentNullException>(() => entryService = new EntryService(mockRepository.Object, mapper, null));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'postValidator')", e.Message);
        Assert.Null(entryService);
    }
    
    [Fact]
    public void CreateEntryService_WithNonNullParamters()
    {
        //Arrange
        var mockRepository = new Mock<IEntryRepository>();
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostEntryDTO, Entry>();
        }).CreateMapper();
        var validator = new PostEntryDTOValidator();
        
        //Act
        IEntryService entryService = new EntryService(mockRepository.Object, mapper, validator);

        //Assert
        Assert.NotNull(entryService);
        Assert.True(entryService is EntryService);
        //TODO: Test if the parameters were truly injected
    }

    [Fact]
    public void Create()
    {
        var mockRepository = new Mock<IEntryRepository>();
        List<Entry> mockEntries = new List<Entry>();
        Entry entry = new Entry() {
            Timestamp = DateTime.Now,
            ItemId = 1,
            ItemName = "Item",
            Change = 1,
            QuantityAfterChange = 1
        };
        PostEntryDTO dto = new PostEntryDTO() {                 
            Timestamp = DateTime.Now,
            ItemId = 1,
            ItemName = "Item",
            Change = 1,
            QuantityAfterChange = 1
            
        };

        mockRepository.Setup(r => r.Create(It.IsAny<Entry>())).Returns(() =>
        {
            mockEntries.Add(entry);
            return entry;
        });
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostEntryDTO, Entry>();
        }).CreateMapper();
        var validator = new PostEntryDTOValidator();
        
        IEntryService entryService = new EntryService(mockRepository.Object,mapper, validator);
        
        //Act
        Entry result = entryService.Create(dto);
        
        //Assert
        Assert.NotNull(result);
        Assert.True(result is Entry);
        Assert.Equal(entry, result);
        mockRepository.Verify(r => r.Create(It.IsAny<Entry>()), Times.Once);
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
        var mapper = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<PostEntryDTO, Entry>();
        }).CreateMapper();
        var validator = new PostEntryDTOValidator();
        
        IEntryService entryService = new EntryService(mockRepository.Object,mapper, validator);
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