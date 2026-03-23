using Application.DTOs;
using Application.Interfaces;
using Domain.Models;

namespace Application.Features.AuthFeatures;

public class UserAuthFeatures(IUserAuthRepository _userAuthRepository)
{

    public async Task<User> GetUserAuthCredentialsAsync(string name, string password)
    {


        if (name == null || name == string.Empty || name == "")
        {
            return null;
        }
        else
        {
            var result = await _userAuthRepository.GetUserAuthCredentialsAsync(name);

            return result;
        }
    }
}
