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
}