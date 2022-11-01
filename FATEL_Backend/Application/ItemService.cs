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
    private IValidator<PostItemDTO> _validator;

    public ItemService(IItemRepository itemRepository, IMapper mapper, IValidator<PostItemDTO> validator)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }
    
    public Item Create(PostItemDTO postItemDto)
    {
        var validation = _validator.Validate(postItemDto);
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

    public Item Update(int id, Item item)
    {
        throw new NotImplementedException();
    }

    public Item Delete(int id)
    {
        return _itemRepository.Delete(id);
    }
}