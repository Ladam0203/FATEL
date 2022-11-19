using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;
    
    public ItemRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        /*
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            Rebuild();
            Seed();
        }
        */
    }

    private void Rebuild()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    private void Seed()
    {
        Item doorKnob = new Item()
        {
            Name = "Pánt", 
            Unit = Unit.Piece,
            Quantity = 5
        };
        Item plank = new Item()
        {
            Name = "Fenyőpadléc", 
            Length = 5,
            Unit = Unit.Meter,
            Quantity = 2
        };
        Item floor1 = new Item()
        {
            Name = "Fenyőlambéria 12x95",
            Length = 5,
            Width = 0.0095f,
            Unit = Unit.SquareMeter,
            Quantity = 2,
            Note = "Szar minőségű"
        };
        Item floor2 = new Item()
        {
            Name = "Fenyőlambéria 12x95",
            Length = 2,
            Width = 0.0095f,
            Unit = Unit.SquareMeter,
            Quantity = 2,
        };
        _context.ItemTable.Add(doorKnob);
        _context.ItemTable.Add(plank);
        _context.ItemTable.Add(floor1);
        _context.ItemTable.Add(floor2);
        _context.SaveChanges();
    }

    public Item Create(Item item)
    {
        _context.ItemTable.Add(item);
        _context.SaveChanges();
        return item;
    }

    public Item Read(int id)
    {
        return _context.ItemTable.Find(id) ?? throw new KeyNotFoundException("Item with id " + id + " does not exist");
    }

    public List<Item> ReadAll()
    {
        return _context.ItemTable.ToList();
    }

    public Item Update(Item item)
    {
        if (_context.ItemTable.Find(item.Id) == null)
            throw new KeyNotFoundException("Item with id " + item.Id + " does not exist");
        _context.ChangeTracker.Clear();
        _context.ItemTable.Update(item);
        _context.SaveChanges();
        return item;
    }

    public Item Delete(int id)
    {
        Item item = _context.ItemTable.Find(id) ?? throw new KeyNotFoundException("Item with id " + id + " does not exist");
        _context.ItemTable.Remove(item);
        _context.SaveChanges();
        return item;
    }

    public double ReadTotalQuantityOf(Item item)
    {
        return _context.ItemTable.Where(i => i.Name == item.Name && i.WarehouseId == item.WarehouseId)
            .Select(i => i.Length.GetValueOrDefault(1) * i.Width.GetValueOrDefault(1) * i.Quantity)
            .Sum();
    }

    public bool DoesIdenticalExist(Item item)
    {
        return _context.ItemTable.Any(i => i.Name == item.Name && 
                                           i.Width == item.Width && 
                                           i.Length == item.Length &&
                                           i.Unit == item.Unit &&
                                           i.WarehouseId == item.WarehouseId);
    }
}
