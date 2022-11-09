using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using Newtonsoft.Json;
using ValidationException = FluentValidation.ValidationException;

namespace Application;

public class ItemService : IItemService
{
    private readonly IRepositoryFacade _repository;
    private IMapper _mapper;
    private IValidator<PostItemDTO> _postValidator;
    private IValidator<PutItemDTO> _putValidator;
    private readonly IValidator<Movement> _movementValidator;

    public ItemService(IRepositoryFacade repository, IMapper mapper, IValidator<PostItemDTO> postValidator, IValidator<PutItemDTO> putValidator, IValidator<Movement> movementValidator)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _postValidator = postValidator ?? throw new ArgumentNullException(nameof(postValidator));
        _putValidator = putValidator ?? throw new ArgumentNullException(nameof(putValidator));
        _movementValidator = movementValidator ?? throw new ArgumentNullException(nameof(movementValidator));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public Item Create(PostItemDTO postItemDto)
    {
        var validation = _postValidator.Validate(postItemDto);
        if (!validation.IsValid) 
            throw new ValidationException(validation.ToString());
        var item = _mapper.Map<Item>(postItemDto);
        if (_repository.DoesIdenticalItemExist(item))
        {
            throw new ArgumentException("Item with the same properties already exists");
        }
        if (item.Quantity == 0)
        {
            return _repository.CreateItem(item);
        }
        double totalChange = item.Length.GetValueOrDefault(1) * item.Width.GetValueOrDefault(1) * item.Quantity;
        Entry entry = new()
        {
            Timestamp = DateTime.Now.ToUniversalTime(),
            //We cannot deal with the Id here
            ItemName = item.Name,
            Change = totalChange,
            QuantityAfterChange = totalChange //old total quantity plus new totalChange
        };
        return _repository.CreateAndRecord(item, entry);
    }

    public Item Read(int id)
    {
        return _repository.ReadItem(id);
    }

    public List<Item> ReadAll()
    {
        return _repository.ReadAllItems();
    }

    public Item Update(int id, PutItemDTO dto)
    {
        if (id != dto.Id)
            throw new ValidationException("Id in route must match Id in request");
        var validation = _putValidator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Item item = _mapper.Map<Item>(dto);
        return _repository.UpdateItem(item);
    }
    
    public Item UpdateQuantity(int id, Movement movement)
    {
        if (id != movement.Item.Id)
            throw new ValidationException("Id in route must match Id in request");
        var validation = _movementValidator.Validate(movement);
        if (!validation.IsValid) 
            throw new ValidationException(validation.ToString());
        
        //Check if item with id exists, if not, this will throw a KeyNotFoundException
        Item item = _repository.ReadItem(movement.Item.Id);

        //Check if stored item is the same as the one in the movement
        if (JsonConvert.SerializeObject(item) != JsonConvert.SerializeObject(movement.Item)) 
            throw new ValidationException("Item in body does not match with stored Item");

        //Change the item based on the movement
        item.Quantity += movement.Change;
        //Create a diary entry
        double totalChange = item.Length.GetValueOrDefault(1) * item.Width.GetValueOrDefault(1) * movement.Change;
        Entry entry = new()
        {
            Timestamp = DateTime.Now.ToUniversalTime(),
            ItemId = item.Id,
            ItemName = item.Name,
            Change = totalChange,
            QuantityAfterChange = _repository.ReadTotalQuantityOf(item.Name) + totalChange //old total quantity plus new totalChange
        };
        return _repository.UpdateQuantityAndRecord(item, entry);
    }

    public Item Delete(int id)
    {
        Item item = _repository.ReadItem(id);
        if (item.Quantity == 0)
        {
             return _repository.DeleteItem(item.Id);
        }

        double totalChange = item.Length.GetValueOrDefault(1) * item.Width.GetValueOrDefault(1) * item.Quantity * -1;
        Entry entry = new()
        {
            Timestamp = DateTime.Now.ToUniversalTime(),
            ItemId = item.Id,
            ItemName = item.Name,
            Change = totalChange,
            QuantityAfterChange = 0  
        };
        return _repository.DeleteAndRecord(item.Id, entry);
    }
}