using Application.AuthentificationRequestBody;
using Application.Features.AuthFeatures;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TicketingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration _configuration, UserAuthFeatures userAuthFeatures) : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult<string>> Authentificate(AuthentificationRequestBody requestBody)
    {
        var user = await ValidateUserCredentials(requestBody.Email, requestBody.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentification:SecretKeyFor"]));

        var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>()
        {
            new Claim("Name", user.Name),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
            new Claim("Email", user.Email)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentification:Issuer"],
            _configuration["Authentification:Audience"],
            claimsForToken,
            DateTime.Now,
            DateTime.Now.AddHours(1),
            signInCredentials
            );

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return Ok(tokenToReturn);
    }

    private Task<User> ValidateUserCredentials(string name, string password)
    {
        return userAuthFeatures.GetUserAuthCredentialsAsync(name, password);
    }
}