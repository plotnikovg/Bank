using System.ComponentModel.DataAnnotations;
using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.UserAggregate;

public class User : BaseEntity, IAggregateRoot
{
    [Required]
    public string Login { get; private set; }
    public string Password { get; private set; }
    public Roles Role { get; private set; }

    protected User()
    {
        
    }
    private User(string login, string password, Roles role)
    {
        Login = login;
        Password = password;
        Role = role;
    }

    public static User CreateAdminAccount(string login,
        string password)
    {
        return new User(login,
            password,
            Roles.Admin);
    }
    public static User CreateManagerAccount(string login,
        string password)
    {
        return new User(login,
            password,
            Roles.Manager);
    }
    public static User CreatClientAccount(string login,
        string password)
    {
        return new User(login,
            password,
            Roles.Client);
    }
}