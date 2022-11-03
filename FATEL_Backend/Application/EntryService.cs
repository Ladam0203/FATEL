using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;

namespace Application;

public class EntryService : IEntryService
{
    
    private readonly IEntryRepository _entryRepository;
    private IMapper _mapper;
    private IValidator<PostEntryDTO> _postValidator;
    
    public EntryService(IEntryRepository entryRepository, IMapper mapper, IValidator<PostEntryDTO> postValidator)
    {
        _entryRepository = entryRepository ?? throw new ArgumentNullException(nameof(entryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _postValidator = postValidator ?? throw new ArgumentNullException(nameof(postValidator));
    }
    
    public Entry Create(PostEntryDTO dto)
    {
        var validation = _postValidator.Validate(dto);
        if (!validation.IsValid) 
            throw new ValidationException(validation.ToString());
        return _entryRepository.Create(_mapper.Map<Entry>(dto));
    }

    public List<Entry> ReadAll()
    {
        return _entryRepository.ReadAll();
    }
}