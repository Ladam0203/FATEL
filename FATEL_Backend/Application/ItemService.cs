using Application.DTOs;
using Application.Interfaces;
using Domain;

namespace Application;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }
    
    public Item Create(PostItemDTO postClientDto)
    {
        throw new NotImplementedException();
    }

    public Item Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Item> ReadAll()
    {
        throw new NotImplementedException();
    }

    public Item Update(string id, Item item)
    {
        throw new NotImplementedException();
    }

    public Item Delete(string id)
    {
        throw new NotImplementedException();
    }
}