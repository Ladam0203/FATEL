using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class ItemRepository : IItemRepository
{
    private AppDbContext _context;
    
    public ItemRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
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

    public Item Delete(int id)
    {
        throw new NotImplementedException();
    }
}