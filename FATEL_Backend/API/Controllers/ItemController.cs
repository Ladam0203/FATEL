using Application.DTOs;
using Application.Interfaces;
using Domain;
using FluentValidation;
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
    public ActionResult<Item> Read([FromRoute] int id)
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
    public ActionResult<Item> Delete([FromRoute] int id)
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
    
    [HttpPost]
    [Route("Create")]
    public ActionResult<Item> Create(PostItemDTO itemDto)
    {
        try
        {
            var item = _itemService.Create(itemDto);
            return Created($"item/{item.Id}", item);
        }
        catch(ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPut]
    [Route("Update/{id}")]
    public ActionResult<Item> Update([FromRoute] int id, [FromBody] PutItemDTO item)
    {
        try
        {
            var result = _itemService.Update(id, item);
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
    
    [HttpPut]
    [Route("UpdateQuantity/{id}")]
    public ActionResult<Item> UpdateQuantity([FromRoute] int id, [FromBody] Movement movement)
    {
        try
        {
            var result = _itemService.UpdateQuantity(id, movement);
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
}