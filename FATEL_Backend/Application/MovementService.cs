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
        Item item = _itemRepository.Read(movement.Item.Id);
        
        bool haveSameData = false;
        foreach(PropertyInfo prop in item.GetType().GetProperties())
        {
            haveSameData = prop.GetValue(item, null)!.Equals(prop.GetValue(movement.Item, null));

            if (!haveSameData)
                throw new ValidationException("Item in body does not match with stored Item");
        }

        return _movementRepository.Record(movement);
    }
}