using Domain.Models;

namespace Application.Interfaces;

public interface IUserDetailsRepository
{
    public Task<bool> UpdateUserDetailsAsync(UserDetails userDetails);
}
