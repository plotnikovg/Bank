using Microsoft.AspNetCore.Identity;

namespace Bank.Application.Interfaces;

public interface IJwtGenerator
{
    string CreateToken(IdentityUser user);
}