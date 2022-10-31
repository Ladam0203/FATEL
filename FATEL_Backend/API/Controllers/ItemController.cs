using Application;
using Application.DTOs;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("api/[Controller]")]
public class ItemController : ControllerBase
{
    private ItemService _itemService;

    public ItemController(ItemService itemService)
    {
        _itemService = itemService;
    }
    
    [HttpGet]
    [Route("Read")]
    public ActionResult<Item> Read(int id)
    {
        return Ok(_itemService.Read(id));
    }
    
    [HttpGet]
    [Route("ReadAll")]
    public ActionResult<List<Item>> CreateItem()
    {
        return Ok(_itemService.ReadAll());
    }
}