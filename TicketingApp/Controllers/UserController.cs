using Application.Common;
using Application.DTOs;
using Application.DTOs.AdminDtos;
using Application.DTOs.PasswordDtos;
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
    [HttpGet("profile")]
    public Result<UserDto> GetUserAsync()
    {
        var result = userFeatures.GetUserProfileAsync(User);

        return result;
    }
    [HttpGet]
    public async Task<Result<IReadOnlyList<UserDto>>> GetAllUserAsync()
    {
        var result = await userFeatures.GetUsersAsync();

        return result;
    }
    [HttpGet("administrators")]
    public async Task<Result<IReadOnlyList<UserDto>>> GetAllAdminUserAsync()
    {
        var result = await userFeatures.GetAdminUsersAsync();

        return result;
    }
    [HttpPut]
    public async Task<Result<bool>> UpdateUserAsync(UserDto user)
    {
        var result = await userFeatures.UpdateUserAsync(User, user);
        return result;
    }
    [HttpPut("admin")]
    public async Task<Result<bool>> UpdateAdminUserAsync(AdminDto user)
    {
        var result = await userFeatures.UpdateAdminUserAsync(User, user);

        return result;
    }
    [HttpPut("password")]
    public async Task<Result<bool>> UpdatePasswordAsync(PasswordDto passwords)
    {
        var result = await userFeatures.UpdatePasswordAsync(User, passwords);

        return result;
    }
}