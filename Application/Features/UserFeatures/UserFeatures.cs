using Application.DTOs;
using Application.Interfaces;
using Domain.Models;

namespace Application.Features.UserFeatures;

public class UserFeatures(IUserDetailsRepository userRepository)
{
    public async Task<bool> AddUserAsync(UserDto user)
    {
        if (user == null)
        {
            return false;
        }
        else
        {
            var temp = new User()
            {
                Name = user.name,
                FirstName = user.firstName,
                LastName = user.lastName,
                Email = user.email
            };
            bool result = await userRepository.AddUserAsync(temp);

            return result;
        }
    }
    public async Task<IReadOnlyList<UserDto>> GetUsersAsync()
    {
        var users = await userRepository.GetUsersAsync();

        if (users == null || users.Count == 0)
        {
            return new List<UserDto>();
        }

        return users.Select(u => new UserDto(u.Name, u.FirstName, u.LastName, u.Email)).ToList();
    }
    public async Task<UserDto> GetUserAsync(string username)
    {
        var user = await userRepository.GetUserAsync(username);

        if (user == null)
        {
            return new UserDto(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        return new UserDto(user.Name, user.FirstName, user.LastName, user.Email);
    }

    public async Task<bool> UpdateUserAsync(string userName, UserDto user)
    {
        bool result = await userRepository.UpdateUserAsync(userName, user);

        return result;
    }
}
