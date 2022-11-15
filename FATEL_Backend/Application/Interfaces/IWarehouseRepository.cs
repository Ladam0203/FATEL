using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IWarehouseRepository
{
    Warehouse Create(PostWarehouseDTO dto);
    List<Warehouse> ReadAll();
    //Read???
    Warehouse Update(PutWarehouseDTO dto);
    Warehouse Delete(int id);
}