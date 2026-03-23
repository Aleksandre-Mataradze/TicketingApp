using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories;

public class UserAuthRepository(TicketingAppDBContext _dbContext) : IUserAuthRepository
{
    public async Task<User> GetUserAuthCredentialsAsync(string name)
    {
        var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == name);

        if (result == null)
        {
            return null;
        }
        else
        {
            return result;
        }
    }
}