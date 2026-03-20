using Application.DTOs;
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
        var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == username);
        
        return result!;
    }

    public async Task<IReadOnlyList<User>> GetUsersAsync()
    {
        var result = await _dbContext.Users.ToListAsync();

        return result!;
    }
    
    public Task<bool> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}
