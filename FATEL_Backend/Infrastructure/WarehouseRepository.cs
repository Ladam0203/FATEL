using Application.DTOs;
using Application.Interfaces;
using Domain;

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
        throw new NotImplementedException();
    }

    public List<Warehouse> ReadAll()
    {
        throw new NotImplementedException();
    }

    public Warehouse Update(Warehouse warehouse)
    {
        throw new NotImplementedException();
    }

    public Warehouse Delete(int id)
    {
        throw new NotImplementedException();
    }
}