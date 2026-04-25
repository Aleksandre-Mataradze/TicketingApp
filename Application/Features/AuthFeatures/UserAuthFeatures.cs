using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.AuthFeatures;

public class UserAuthFeatures(IUserAuthRepository _userAuthRepository, IPasswordHasher<User> passwordHasher)
{

    public async Task<Result<User>> GetUserAuthCredentialsAsync(string name, string password)
    {


        if (name == null || name == string.Empty || name == "")
        {
            return Result<User>.Fail("There was not input.");
        }
        else
        {

            var user = await _userAuthRepository.GetUserAuthCredentialsAsync(name);

            if (user == null)
            {
                return Result<User>.Fail("User not found.");
            }
            else
            {
                var verifyResult = passwordHasher.VerifyHashedPassword(user, user.Password, password);

                if (verifyResult == PasswordVerificationResult.Success)
                {
                    return Result<User>.Ok(user);
                }
                else
                {
                    return Result<User>.Fail("Password is incorrect!");
                }
            }
        }
    }
}
