using Application.Common;
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
    [AllowAnonymous]
    [HttpPost]
    public async Task<Result<bool>> AddUserAsync(UserCreationDto user)
    {
        var result = await userFeatures.AddUserAsync(user);

        return result;
    }

    [HttpGet("{username}")]
    public async Task<Result<UserDto>> GetUserAsync(string username)
    {
        var result = await userFeatures.GetUserAsync(username);

        return result;
    }

    [HttpGet]
    public async Task<Result<IReadOnlyList<UserDto>>> GetAllUserAsync()
    {
        var result = await userFeatures.GetUsersAsync();

        return result;
    }

    [HttpPut]
    public async Task<Result<bool>> UpdateUserAsync(string userName, UserDto user)
    {
        var result = await userFeatures.UpdateUserAsync(userName, user);
        return result;
    }
}