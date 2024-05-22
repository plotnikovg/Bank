namespace Bank.Application.Models;

public record UserViewModel
{
    public string UserName { get; init; }
    public string Token { get; init; }
}