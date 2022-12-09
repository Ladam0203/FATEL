using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IWarehouseService
{
    Warehouse Create(PostWarehouseDTO dto);
    List<Warehouse> ReadAll();
    Warehouse Update(int id, PutWarehouseDTO dto);
    Warehouse Delete(int id);
}