using System.Reflection;
using Application.Interfaces;
using Domain;
using FluentValidation;

namespace Application;

public class MovementService : IMovementService
{
    private readonly IItemRepository _itemRepository;
    private readonly IMovementRepository _movementRepository;

    public MovementService(IItemRepository itemRepository, IMovementRepository movementRepository)
    {
        _itemRepository = itemRepository;
        _movementRepository = movementRepository;
    }

    public Entry Record(Movement movement)
    {
        //Check if item with id exists
        Item item = _itemRepository.Read(movement.Item.Id);

        //TODO: Check if stored item is the same as the item in the request IS THIS REALLY NECESSARY?
        /*
        bool haveSameData = false;
        foreach(PropertyInfo prop in item.GetType().GetProperties())
        {
            haveSameData = prop.GetValue(item, null).Equals(prop.GetValue(movement.Item, null));

            if (!haveSameData)
                throw new ValidationException("Item in body does not match with stored Item");
        }
        */
        
        //TODO: CHANGE CANNOT BE ZERO OR NULL
        //Change the item based on the movement
        item.Quantity += movement.Change;
        //Create a diary entry
        Entry entry = new()
        {
            Timestamp = DateTime.Now.ToUniversalTime(),
            ItemId = item.Id,
            ItemName = item.Name,
            Change = movement.Change,
            QuantityAfterChange = _itemRepository.ReadTotalQuantityOf(item.Name) + movement.Change
        };
        return _movementRepository.Record(item, entry);
    }
}