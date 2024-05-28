using Microsoft.AspNetCore.Identity;

namespace Bank.Application.Interfaces;

public interface IJwtGenerator
{
    string CreateToken(string userId, List<string> roles, string userIp, string userBrowser);
}