using Application.Common;
using Application.DTOs;
using Application.DTOs.AdminDtos;
using Application.DTOs.PasswordDtos;
using Application.DTOs.UserDtos;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.Features.UserFeatures;

public class UserFeatures(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
{
    public async Task<Result<bool>> AddUserAsync(UserCreationDto user)
    {
        if (user == null)
        {
            return Result<bool>.Fail("Not all fields are filled in");
        }
        else
        {
            var temp = new User()
            {
                Name = user.name,
                FirstName = user.firstName,
                LastName = user.lastName,
                Email = user.email,
                Password = user.password
            };

            temp.Password = passwordHasher.HashPassword(temp, temp.Password);

            bool result = await userRepository.AddUserAsync(temp);

            return Result<bool>.Ok(result);
        }
    }
    public async Task<Result<IReadOnlyList<UserDto>>> GetUsersAsync()
    {
        var users = await userRepository.GetUsersAsync();

        if (users == null || users.Count == 0)
        {
            return Result<IReadOnlyList<UserDto>>.Fail("User list not found");
        }

        var result = users.Select(u => new UserDto(u.Name, u.FirstName, u.LastName, u.Email, u.Admin.ToString())).ToList();

        return Result<IReadOnlyList<UserDto>>.Ok(result);
    }
    public async Task<Result<IReadOnlyList<UserDto>>> GetAdminUsersAsync()
    {
        var userList = await userRepository.GetUsersAsync();

        if (userList == null || userList.Count == 0)
        {
            return Result<IReadOnlyList<UserDto>>.Fail("User list not found.");
        }

        var result = userList.Where(u => u.Admin == true).Select(u => new UserDto(u.Name, u.FirstName, u.LastName, u.Email, u.Admin.ToString())).ToList();

        return Result<IReadOnlyList<UserDto>>.Ok(result);
    }
    public async Task<Result<UserDto>> GetUserAsync(string username)
    {
        var user = await userRepository.GetUserAsync(username);

        if (user == null)
        {
            return Result<UserDto>.Fail("User not found");
        }

        var tempUser = new UserDto(user.Name, user.FirstName, user.LastName, user.Email, user.Admin.ToString());

        return Result<UserDto>.Ok(tempUser);
    }
    public async Task<Result<bool>> UpdateUserAsync(ClaimsPrincipal user, UserDto data)
    {
        var userName = user.FindFirst("Name")?.Value;

        bool result = await userRepository.UpdateUserAsync(userName, data);

        return Result<bool>.Ok(result);
    }
    public Result<UserDto> GetUserProfileAsync(ClaimsPrincipal user)
    {
        var name = user.FindFirst("Name")?.Value;
        var firstName = user.FindFirst("FirstName")?.Value;
        var lastName = user.FindFirst("LastName")?.Value;
        var email = user.FindFirst("Email")?.Value;
        var admin = user.FindFirst("Admin")?.Value;

        if (string.IsNullOrEmpty(name))
        {
            return Result<UserDto>.Fail("User information not found in token.");
        }

        var userDto = new UserDto(name, firstName!, lastName!, email!, admin!);
        return Result<UserDto>.Ok(userDto);
    }
    public async Task<Result<bool>> UpdateAdminUserAsync(ClaimsPrincipal user, AdminDto data)
    {
        var userName = user.FindFirst("Name")?.Value;

        bool result = await userRepository.UpdateAdminUserAsync(userName, data);

        return Result<bool>.Ok(result);
    }
    public async Task<Result<bool>> UpdatePasswordAsync(ClaimsPrincipal user, PasswordDto passwords)
    {
        var userName = user.FindFirst("Name")?.Value;

        var existingUser = await userRepository.GetUserAsync(userName);
        if (existingUser == null)
        {
            return Result<bool>.Fail("User not found.");
        }

        var verificationResult = passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, passwords.oldPassword);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return Result<bool>.Fail("The old password you entered is incorrect.");
        }

        existingUser.Password = passwordHasher.HashPassword(existingUser, passwords.newPassword);

        bool result = await userRepository.UpdatePasswordAsync(existingUser.Name, existingUser.Password);

        return Result<bool>.Ok(result);
    }
    public async Task<Result<bool>> changeAdminRoleAsync(RoleAssignmentDto data)
    {
        if (data.roleName == "Admin")
        {
            var result = await userRepository.changeAdminRoleAsync(data.userName, true);

            return Result<bool>.Ok(result);
        }
        else if (data.roleName == "User")
        {
            var result = await userRepository.changeAdminRoleAsync(data.userName, false);

            return Result<bool>.Ok(result);
        }
        else
        {
            return Result<bool>.Fail("Failed to change a role");
        }
    }
}