using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class ItemRepository : IItemRepository
{
    private DbContext _context;
    
    public ItemRepository(DbContext context)
    {
        _context = context;
    }
    
    public Item Create(Item item)
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

    public Item Update(Item item)
    {
        throw new NotImplementedException();
    }

    public Item Delete(string id)
    {
        throw new NotImplementedException();
    }
}