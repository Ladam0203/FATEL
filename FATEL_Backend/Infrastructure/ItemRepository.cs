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

        Item test1 = new Item() { Name = "Item1", Quantity = 1 };
        Item test2 = new Item() { Name = "Item2", Quantity = 1 };
        Item test3 = new Item() { Name = "Item3", Quantity = 1 };
        _context.ItemTable.Add(test1);
        _context.ItemTable.Add(test2);
        _context.ItemTable.Add(test3);
        _context.SaveChanges();
    }
    
    //TODO: Create a separate Seed method
    
    public Item Create(Item item)
    {
        _context.ItemTable.Add(item);
        _context.SaveChanges();
        return item;
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
        //TODO: Most probably we need a find here actually
        _context.ChangeTracker.Clear();
        _context.ItemTable.Update(item);
        _context.SaveChanges();
        return item;
    }

    public Item Delete(int id)
    {
        Item item = _context.ItemTable.Find(id);
        if (item != null)
        {
            _context.ItemTable.Remove(item);
            _context.SaveChanges();
            return item;
        }
        throw new KeyNotFoundException(); //TODO: Write message
    }
}