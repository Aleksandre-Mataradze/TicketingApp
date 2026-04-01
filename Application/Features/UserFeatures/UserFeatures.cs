using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

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

        var result = users.Select(u => new UserDto(u.Name, u.FirstName, u.LastName, u.Email)).ToList();

        return Result<IReadOnlyList<UserDto>>.Ok(result);
    }
    public async Task<Result<UserDto>> GetUserAsync(string username)
    {
        var user = await userRepository.GetUserAsync(username);

        if (user == null)
        {
            return Result<UserDto>.Fail("User not found");
        }

        var tempUser = new UserDto(user.Name, user.FirstName, user.LastName, user.Email);

        return Result<UserDto>.Ok(tempUser);
    }
    public async Task<Result<bool>> UpdateUserAsync(string userName, UserDto user)
    {
        bool result = await userRepository.UpdateUserAsync(userName, user);

        return Result<bool>.Ok(result);
    }
}