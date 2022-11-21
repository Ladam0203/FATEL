using Application.DTOs;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly AppDbContext _context;
    
    public WarehouseRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        //Rebuild();
        //SeedWarehouse();
        //SeedItems();
        //SeedDiary();
    }
    
    public Warehouse Create(Warehouse warehouse)
    {
        _context.WarehouseTable.Add(warehouse);
        _context.SaveChanges();
        return warehouse;
    }

    public List<Warehouse> ReadAll()
    {
        return _context.WarehouseTable
            .Include(w => w.Inventory)
            .Include(w => w.Diary)
            .ToList();
    }

    public Warehouse Update(Warehouse warehouse)
    {
        if (_context.WarehouseTable.Find(warehouse.Id) == null)
            throw new KeyNotFoundException("Warehouse with the Id: " + warehouse.Id + " does not exist.");
        _context.ChangeTracker.Clear();
        _context.WarehouseTable.Update(warehouse);
        _context.SaveChanges();
        return warehouse;
    }

    public Warehouse Delete(int id)
    {
        Warehouse warehouse = _context.WarehouseTable.Find(id) ?? throw new KeyNotFoundException("Warehouse with id " + id + " does not exist");
        _context.WarehouseTable.Remove(warehouse);
        _context.SaveChanges();
        return warehouse;
    }
    
    private void Rebuild()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    private void SeedWarehouse()
    {
        Warehouse warehouse1 = new Warehouse()
        {
            Name = "Warehouse1",
            Diary = new List<Entry>(),
            Inventory = new List<Item>()
        };
        _context.WarehouseTable.Add(warehouse1);
        _context.SaveChanges();
    }
    
    private void SeedItems()
    {
        Item doorKnob = new Item()
        {
            Name = "Pánt", 
            Unit = Unit.Piece,
            Quantity = 5,
            WarehouseId = 1
        };
        Item plank = new Item()
        {
            Name = "Fenyőpadléc", 
            Length = 5,
            Unit = Unit.Meter,
            Quantity = 2,
            WarehouseId = 1
        };
        Item floor1 = new Item()
        {
            Name = "Fenyőlambéria 12x95",
            Length = 5,
            Width = 0.0095f,
            Unit = Unit.SquareMeter,
            Quantity = 2,
            Note = "Szar minőségű",
            WarehouseId = 1
        };
        Item floor2 = new Item()
        {
            Name = "Fenyőlambéria 12x95",
            Length = 2,
            Width = 0.0095f,
            Unit = Unit.SquareMeter,
            Quantity = 2,
            WarehouseId = 1
        };

        _context.ItemTable.Add(doorKnob);
        _context.ItemTable.Add(plank);
        _context.ItemTable.Add(floor1);
        _context.ItemTable.Add(floor2);
        _context.SaveChanges();
    }

    private void SeedDiary()
    {
        Entry entry1 = new Entry()
        {
            WarehouseId = 1,
            Timestamp = DateTime.Now.ToUniversalTime(),
            ItemId = 6,
            ItemName = "Fenyőpadléc",
            Change = 2,
            QuantityAfterChange = 10
        };
        Entry entry2 = new Entry()
        {
            WarehouseId = 1,
            Timestamp = DateTime.Now.ToUniversalTime(),
            ItemId = 6,
            ItemName = "Fenyőpadléc",
            Change = 3,
            QuantityAfterChange = 13
        };
        _context.EntryTable.Add(entry1);
        _context.EntryTable.Add(entry2);
        _context.SaveChanges();
    }
}