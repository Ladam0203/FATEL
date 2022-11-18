using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;

namespace Application;

public class WarehouseService : IWarehouseService

{

    private readonly IRepositoryFacade _repository;
    private IValidator<PostWarehouseDTO> _postValidator;
    private IMapper _mapper;

    public WarehouseService(IRepositoryFacade repository, IValidator<PostWarehouseDTO> postValidator, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _postValidator = postValidator ?? throw new ArgumentNullException(nameof(postValidator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Warehouse Create(PostWarehouseDTO postWarehouseDto)
    {
        var validation = _postValidator.Validate(postWarehouseDto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        //Maybe cech for the same name of warehouses
        var warehouse = _mapper.Map<Warehouse>(postWarehouseDto);

        return _repository.CreateWarehouse(warehouse);

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