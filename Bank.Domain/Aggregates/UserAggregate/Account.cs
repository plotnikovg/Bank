using System.ComponentModel.DataAnnotations;
using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.UserAggregate;

public class Account : BaseEntity, IAggregateRoot
{
    [Required]
    public string Login { get; private set; }
    public string Password { get; private set; }
    public Roles Role { get; private set; }

    public Account(string login, string password, Roles role)
    {
        Login = login;
        Password = password;
        Role = role;
    }

    public Account CreateAdminAccount(string login,
        string password)
    {
        return new Account(login,
            password,
            Roles.Admin);
    }
    public Account CreateManagerAccount(string login,
        string password)
    {
        return new Account(login,
            password,
            Roles.Manager);
    }
    public Account CreatClientAccount(string login,
        string password)
    {
        return new Account(login,
            password,
            Roles.Client);
    }
}