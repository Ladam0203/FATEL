using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class EntryController : ControllerBase
{
    private readonly IEntryService _entryService;

    public EntryController(IEntryService entryService)
    {
        _entryService = entryService;
    }
        
    [HttpGet]
    [Route("ReadAll")]
    public ActionResult<List<Entry>> ReadAll()
    {
        try
        {
            return Ok(_entryService.ReadAll());
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
}