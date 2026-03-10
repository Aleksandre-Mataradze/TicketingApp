using Application.DTOs;
using Application.Interfaces;
using MyUser = Domain.Models.User;

namespace Application.Features.User;

public class UserFeatures(IUserRepository userRepository)
{
    public async Task<IReadOnlyList<UserDto>> GetUsersAsync()
    {
        var users = await userRepository.GetUsersAsync();

        return users.Select(u => new UserDto(u.Name, u.FirstName, u.LastName, u.Email)).ToList();
    }

    public async Task<UserDto> GetUserAsync(string username)
    {
        var user = await userRepository.GetUserAsync(username);
        return new UserDto(user.Name, user.FirstName, user.LastName, user.Email);
    }

    public async Task<bool> AddUserAsync(MyUser user)
    {
        return await userRepository.AddUserAsync(user);
    }
}
