using System.ComponentModel.DataAnnotations;
using Application.DTOs;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet]
    [Route("ReadAll")]
    public ActionResult<List<Warehouse>> ReadAll()
    {
        try
        {
            return Ok(_warehouseService.ReadAll());
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }

    [HttpPost]
    [Route("Create")]
    public ActionResult<Warehouse> Create(PostWarehouseDTO warehouseDto)
    {
        try
        {
            var warehouse = _warehouseService.Create(warehouseDto);
            return Created($"warehouse/{warehouse.Id}", warehouse);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        // catch (ArgumentException e)
        // {
        //     return StatusCode(403, e.Message);
        // }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPut]
    [Route("Update/{id}")]
    public ActionResult<Warehouse> Update([FromRoute] int id, [FromBody] PutWarehouseDTO warehouse)
    {
        try
        {
            var result = _warehouseService.Update(id, warehouse);
            return Ok(result);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("Delete/{id}")]
    public ActionResult<Warehouse> Delete([FromRoute] int id)
    {
        try
        {
            return Ok(_warehouseService.Delete(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
}