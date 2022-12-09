using Domain;

namespace Application.Interfaces;

public interface IWarehouseRepository
{
    Warehouse Create(Warehouse warehouse);
    List<Warehouse> ReadAll();
    Warehouse Update(Warehouse warehouse);
    Warehouse Delete(int id);
}