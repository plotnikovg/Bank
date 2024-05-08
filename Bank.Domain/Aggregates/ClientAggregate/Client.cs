using Bank.Domain.Aggregates.BankAccountAggregate;
using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.ClientAggregate;

//Bank client
public class Client : BaseEntity, IAggregateRoot
{
    public Name Name => Passport.Name;
    public Passport Passport { get; private set; }
    public string PhoneNumber { get; private set; }
    public BankAccount? AccountForReceivingTransfers { get; private set; }
    private readonly List<BankAccount> _bankAccounts;
    public IReadOnlyCollection<BankAccount> BankAccounts => _bankAccounts.AsReadOnly();

    protected Client()
    {
        _bankAccounts = new List<BankAccount>();
    }
    public Client(Passport passport, string phoneNumber)
    {
        Id = Guid.NewGuid();
        Passport = passport;
        PhoneNumber = phoneNumber;
    }

    public void AddBankAccount(BankAccount bankAccount)
    {
        _bankAccounts.Add(bankAccount);
        if (AccountForReceivingTransfers == null) AccountForReceivingTransfers = bankAccount;
    }

    public void SetAccountForReceivingTransfers(BankAccount bankAccount)
    {
        if (!_bankAccounts.Contains(bankAccount)) throw new InvalidOperationException();
        AccountForReceivingTransfers = bankAccount;
    }
}   
