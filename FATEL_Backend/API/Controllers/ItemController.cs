using Application;
using Application.DTOs;
using Application.Validators;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("[Controller]")]
public class ItemController : ControllerBase
{
    private ItemRepository _itemRepository;
    private ItemValidator _itemValidator;
    private IMapper _mapper;

    public ItemController(ItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _itemValidator = new ItemValidator();
        _mapper = mapper;
    }

    [HttpPost]
    public ActionResult CreateItem(PostItemDTO itemDto)
    {
        var validate = _itemValidator.Validate(itemDto);
        if (!validate.IsValid) 
            return BadRequest(validate.ToString());
        var newItem = _mapper.Map<Item>(itemDto);
        return Ok(_itemRepository.Create(newItem));

    }



}