using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;

namespace Application;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private IMapper _mapper;
    private IValidator<PostItemDTO> _postValidator;
    private IValidator<PutItemDTO> _putValidator;

    public ItemService(IItemRepository itemRepository, IMapper mapper, IValidator<PostItemDTO> validator, IValidator<PutItemDTO> putValidator)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _postValidator = validator ?? throw new ArgumentNullException(nameof(validator));
        _putValidator = putValidator ?? throw new ArgumentNullException(nameof(putValidator));
    }
    
    public Item Create(PostItemDTO postItemDto)
    {
        var validation = _postValidator.Validate(postItemDto);
        if (!validation.IsValid) 
            throw new ValidationException(validation.ToString());
        return _mapper.Map<Item>(postItemDto);

    }

    public Item Read(int id)
    {
        return _itemRepository.Read(id);
    }

    public List<Item> ReadAll()
    {
        return _itemRepository.ReadAll();
    }

    public Item Update(int id, PutItemDTO dto)
    {
        if (id != dto.Id)
            throw new ValidationException("Id in route must match Id in request");
        var validation = _putValidator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Item item = _mapper.Map<Item>(dto);
        return _itemRepository.Update(item);
    }

    public Item Delete(int id)
    {
        return _itemRepository.Delete(id);
    }
}