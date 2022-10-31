using Application;
using Application.DTOs;
using Application.Interfaces;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("api/[Controller]")]
public class ItemController : ControllerBase
{
    private IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }
    
    [HttpGet]
    [Route("Read")]
    public ActionResult<Item> Read(int id)
    {
        return _itemService.Read(id);
    }
    
    [HttpGet]
    [Route("ReadAll")]
    public ActionResult<List<Item>> CreateItem()
    {
        return _itemService.ReadAll();
    }
}