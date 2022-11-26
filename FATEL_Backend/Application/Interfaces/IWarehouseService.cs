using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IWarehouseService
{
    public Warehouse Create(PostWarehouseDTO dto);
    public List<Warehouse> ReadAll();
    //Read???
    public Warehouse Update(int id, PutWarehouseDTO dto);
    public Warehouse Delete(int id);
}