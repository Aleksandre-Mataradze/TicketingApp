using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.AuthFeatures;

public class UserAuthFeatures(IUserAuthRepository _userAuthRepository, IPasswordHasher<User> passwordHasher)
{

    public async Task<User> GetUserAuthCredentialsAsync(string name, string password)
    {


        if (name == null || name == string.Empty || name == "")
        {
            return null;
        }
        else
        {

            var user = await _userAuthRepository.GetUserAuthCredentialsAsync(name);

            if (user == null)
            {
                return null;
            }
            else
            {
                var verifyResult = passwordHasher.VerifyHashedPassword(user, user.Password, password);

                if (verifyResult == PasswordVerificationResult.Success)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
