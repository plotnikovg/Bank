using System.ComponentModel.DataAnnotations;
using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.AccountAggregate;

public class Account : BaseEntity, IAggregateRoot
{
    [Required]
    public string Login { get; private set; }
    public string Password { get; private set; }

    public Account(string login, string password)
    {
        Login = login;
        Password = password;
    }
}