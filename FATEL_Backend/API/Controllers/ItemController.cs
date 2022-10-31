using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("api/[Controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }
    
    [HttpGet]
    [Route("Read/{id}")]
    public ActionResult<Item> Read(int id)
    {
        try
        {
            return Ok(_itemService.Read(id));
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No Item found with the id " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    
    [HttpGet]
    [Route("ReadAll")]
    public ActionResult<List<Item>> ReadAll()
    {
        try
        {
            return Ok(_itemService.ReadAll());
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public ActionResult<Item> DeleteItem(int id)
    {
        try
        {
            return Ok(_itemService.Delete(id));
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