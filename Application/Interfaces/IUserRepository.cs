using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface IUserRepository
{
    public Task<bool> AddUserAsync(User user);
    public Task<User> GetUserAsync(string username);
    public Task<IReadOnlyList<User>> GetUsersAsync();
    public Task<bool> UpdateUserAsync(string userName, UserDto user);
}
