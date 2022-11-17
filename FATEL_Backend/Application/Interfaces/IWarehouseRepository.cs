using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IWarehouseRepository
{
    public Warehouse Create(Warehouse warehouse);
    public List<Warehouse> ReadAll();
    //Read???
    public Warehouse Update(Warehouse warehouse);
    public Warehouse Delete(int id);
}