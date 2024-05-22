using Bank.Application.Models;

namespace Bank.Application.Queries;
/// <summary>
/// User authentication
/// </summary>
/// <param name="UserName">Username/Phone number/Email</param>
/// <param name="Password"></param>
/// <returns>Tuple(isSuccess, errorMessage)</returns>
public class LoginQuery : IRequest<Tuple<bool, UserViewModel>>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}