using Application.DTOs;
using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class WarehouseRepository : IWarehouseRepository
{
    public Warehouse Create(PostWarehouseDTO dto)
    {
        throw new NotImplementedException();
    }

    public List<Warehouse> ReadAll()
    {
        throw new NotImplementedException();
    }

    public Warehouse Update(PutWarehouseDTO dto)
    {
        throw new NotImplementedException();
    }

    public Warehouse Delete(int id)
    {
        throw new NotImplementedException();
    }
}