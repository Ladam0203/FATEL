using Application;
using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]

[Route("api/[Controller]")]

public class MovementController : ControllerBase
{
    private readonly IMovementService _movementService;

    public MovementController(IMovementService movementService)
    {
        _movementService = movementService;
    }

    [HttpPost]
    [Route("Record")]
    public ActionResult<Entry> Record(Movement movement)
    {
        try
        {
            var entry = _movementService.Record(movement);
            return Created($"entry/{entry.Id}", entry);
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