using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bank.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Bank.Infrastructure.Identity;

public class JwtGenerator : IJwtGenerator
{
    private readonly SymmetricSecurityKey _key;

    public JwtGenerator(IConfiguration configuration)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
    }
    public string CreateToken(IdentityUser identityUser)
    {
        var claims = new List<Claim>{new Claim(JwtRegisteredClaimNames.NameId, identityUser.UserName)};

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(14),
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}