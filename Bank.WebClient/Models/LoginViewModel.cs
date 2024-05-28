namespace Bank.WebClient.Models;

public class LoginViewModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? Ip { get; set; } = null;
    public string? Browser { get; set; } = null;
}