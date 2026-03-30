using Application.DTOs;
using Application.Features.UserFeatures;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicketingApp.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController(UserFeatures userFeatures) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<bool>> AddUserAsync(UserDto user)
    {
        var result = await userFeatures.AddUserAsync(user);

        return Ok(result);
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

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateUserAsync(string userName, UserDto user)
    {
        var result = await userFeatures.UpdateUserAsync(userName, user);
        return Ok(result);
    }
}