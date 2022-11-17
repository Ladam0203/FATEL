using Application.DTOs;
using Application.Interfaces;
using Domain;

namespace Application;

public class WarehouseService : IWarehouseService

{

    private readonly IRepositoryFacade _repository;
    public WarehouseService(IRepositoryFacade repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public Warehouse Create(PostWarehouseDTO dto)
    {
        throw new NotImplementedException();
    }

    public List<Warehouse> ReadAll()
    {
        return _repository.ReadAllWarehouses();
    }

    public Warehouse Update(int id, PutWarehouseDTO dto)
    {
        throw new NotImplementedException();
    }

    public Warehouse Delete(int id)
    {
        throw new NotImplementedException();
    }
}