using Application.DTOs;
using Application.Features.UserFeatures;
using Microsoft.AspNetCore.Mvc;

namespace TicketingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserFeatures userFeatures) : ControllerBase
{
    [HttpPost]
    public async Task<bool> AddUserAsync(UserDto user)
    {
        return await userFeatures.AddUserAsync(user);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<UserDto>> GetUserAsync(string username)
    {
        var result = await userFeatures.GetUserAsync(username);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAllUserAsync()
    {
        var result = await userFeatures.GetUsersAsync();

        return Ok(result);
    }
}
