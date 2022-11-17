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
}