using Bank.Domain.Aggregates.BankAccountAggregate;
using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.ClientAggregate;

//Bank client
public class Client : BaseEntity, IAggregateRoot
{
    public Name Name { get; private set; }

    private readonly List<BankAccount> _bankAccounts;
    public IReadOnlyCollection<BankAccount> BankAccounts => _bankAccounts.AsReadOnly();
}