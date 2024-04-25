using System.ComponentModel;
using Bank.Domain.Aggregates.BankAccountAggregate;
using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.ClientAggregate;

//Bank client
public class Client : BaseEntity, IAggregateRoot
{
    public Name Name => Passport.Name;
    public Passport Passport { get; private set; }
    private readonly List<BankAccount> _bankAccounts;
    public IReadOnlyCollection<BankAccount> BankAccounts => _bankAccounts.AsReadOnly();

    protected Client()
    {
        _bankAccounts = new List<BankAccount>();
    }
    public Client(Passport passport)
    {
        Passport = passport;
    }

    public void AddBankAccount(BankAccount bankAccount)
    {
        _bankAccounts.Add(bankAccount);
    }
}   
