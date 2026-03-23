using Domain.Models;

namespace Application.Interfaces;

public interface IUserAuthRepository
{
    public Task<User> GetUserAuthCredentialsAsync(string name);
}
