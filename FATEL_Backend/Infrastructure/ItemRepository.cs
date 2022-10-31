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
        Item item = _context.ItemTable.Find(id);
        if (item != null)
            return item;
        throw new KeyNotFoundException();
    }

    public List<Item> ReadAll()
    {
        return _context.ItemTable.ToList();
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