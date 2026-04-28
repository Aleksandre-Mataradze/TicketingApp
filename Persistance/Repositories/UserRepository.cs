using Application.Common;
using Application.DTOs;
using Application.DTOs.AdminDtos;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories;

public class UserRepository(TicketingAppDBContext _dbContext) : IUserRepository
{
    public async Task<bool> AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        var result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false;
    }
    public async Task<User> GetUserAsync(string username)
    {
        var result = await _dbContext.Users.Where(v => v.DeletedAt == null).FirstOrDefaultAsync(u => u.Name == username);
        
        return result!;
    }
    public async Task<IReadOnlyList<User>> GetUsersAsync()
    {
        var result = await _dbContext.Users.Where(v => v.DeletedAt == null).ToListAsync();

        return result!;
    }
    public async Task<bool> UpdateUserAsync(string userName, UserDto user)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == userName);

        existingUser!.Name = user.name;
        existingUser.FirstName = user.firstName;
        existingUser.LastName = user.lastName;
        existingUser.Email = user.email;
        existingUser.Admin = existingUser.Admin;


        int result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false;
    }
    public async Task<bool> UpdateAdminUserAsync(string userName, AdminDto user)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == userName);

        existingUser!.Name = user.userName;
        existingUser.Email = user.email;

        int result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false;
    }
    public Task<bool> UpdateUserDetailsAsync(UserDetails userDetails)
    {
        throw new NotImplementedException();
    }
    public async Task<string> GetHashedPasswordAsync(string user)
    {
        var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == user);

        string password = result.Password;

        return password;
    }
    public async Task<bool> UpdatePasswordAsync(string userName, string newPassword)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == userName);

        user.Password = newPassword;

        var result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false;
    }
    public async Task<bool> changeAdminRoleAsync(string userName, bool role)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == userName);

        if (user == null)
        {
            return false;
        }
        else
        {
            user.Admin = role;

            var result = await _dbContext.SaveChangesAsync();

            return result >= 1 ? true : false;
        }
    }
}