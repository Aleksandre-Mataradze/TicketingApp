using Application.DTOs;
using Application.DTOs.AdminDtos;
using Domain.Models;

namespace Application.Interfaces;

public interface IUserRepository
{
    public Task<bool> AddUserAsync(User user);
    public Task<User> GetUserAsync(string username);
    public Task<IReadOnlyList<User>> GetUsersAsync();
    public Task<bool> UpdateUserAsync(string userName, UserDto user);
    public Task<bool> UpdateAdminUserAsync(string userName, AdminDto user);
    public Task<bool> UpdatePasswordAsync(string user, string newPassword);
    public Task<bool> changeAdminRoleAsync(string userName, bool role);
}
