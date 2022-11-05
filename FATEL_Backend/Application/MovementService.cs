using System.Reflection;
using Application.Interfaces;
using Domain;
using FluentValidation;
using Newtonsoft.Json;

namespace Application;

public class MovementService : IMovementService
{
    private readonly IItemRepository _itemRepository;
    private readonly IMovementRepository _movementRepository;
    private readonly IValidator<Movement> _movementValidator;

    public MovementService(IItemRepository itemRepository, IMovementRepository movementRepository, IValidator<Movement> movementValidator)
    {
        _itemRepository = itemRepository;
        _movementRepository = movementRepository;
        _movementValidator = movementValidator;
    }

    public Entry Record(Movement movement)
    {
        var validation = _movementValidator.Validate(movement);
        if (!validation.IsValid) 
            throw new ValidationException(validation.ToString());
        
        //Check if item with id exists, if not, this will throw a KeyNotFoundException
        Item item = _itemRepository.Read(movement.Item.Id);

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
            QuantityAfterChange = _itemRepository.ReadTotalQuantityOf(item.Name) + totalChange //old total quantity plus new totalChange
        };
        return _movementRepository.Record(item, entry);
    }
}