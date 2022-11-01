using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;
    
    public ItemRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    private void Rebuild()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
    
    public Item Create(Item item)
    {
        throw new NotImplementedException();
    }

    public Item Read(int id)
    {
        Item item = _context.ItemTable.Find(id);
        return item ?? throw new KeyNotFoundException();
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