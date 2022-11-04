using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;

namespace Application;

public class EntryService : IEntryService
{
    
    private readonly IEntryRepository _entryRepository;

    public EntryService(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository ?? throw new ArgumentNullException(nameof(entryRepository));
    }


    public List<Entry> ReadAll()
    {
        return _entryRepository.ReadAll();
    }
}