namespace Bank.API.Models;
/// <summary>
/// 
/// </summary>
/// <param name="UserName">Может содержать как номер телефона, так и email или username</param>
/// <param name="Password">Пароль</param>
public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Ip { get; set; }
    public string Browser { get; set; }
}