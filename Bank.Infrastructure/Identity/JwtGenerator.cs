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
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hru3j7fz2k91k7g467dgte543mnf7g4f8h9fj7ydfg6789dfgdhfkghdfg67823p29375khdfg6"));
    }
    public string CreateToken(string userId, List<string> roles, string userIp, string userBrowser)
    {
        //Сбор данных пользователя
        var claims = new List<Claim>
        {
            //Id
            new Claim(JwtRegisteredClaimNames.NameId, userId),
            //Ip
            new Claim(JwtRegisteredClaimNames.Prn, userIp),
            //Браузер
            new Claim(JwtRegisteredClaimNames.Aud, userBrowser)
        };
        foreach (var role in roles)
            //Роли
            claims.Add(new Claim(ClaimTypes.Role, role));

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        
        //tokenDescriptor содержит информацию для создания токена
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(14),
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        //Создание токена
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}