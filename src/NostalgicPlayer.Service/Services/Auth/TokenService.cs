using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NostalgicPlayer.Domain.Entities.Users;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.Interfaces.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NostalgicPlayer.Service.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration _config)
    {
        _configuration = _config.GetSection("Jwt");
    }
    public async Task<string> GenerateToken(User user)
    {
        var identityClaims = new Claim[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.Role, user.IdentityRole.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        int expiresHours = int.Parse(_configuration["Lifetime"]!);
        var token = new JwtSecurityToken(
            issuer: _configuration["Issuer"],
            audience: _configuration["Audience"],
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}