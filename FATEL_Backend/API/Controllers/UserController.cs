using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("Login")]
    public IActionResult Login([FromBody] LoginDTO model)
    {
        string userToken;
        if (_userService.Login(model.Username, model.Password, out userToken))
        {
            return Ok(userToken);
        }
        return Unauthorized("Username or password is incorrect");
    }
}