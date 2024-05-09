using Bank.Domain.Aggregates.ClientAggregate;
using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.BankAccountAggregate;

public class BankAccount : BaseEntity
{
    public Money Balance { get; private set; }
    public decimal WithdrawalLimit { get; private set; } //Лимит на снятие средств
    public bool IsBlocked { get; private set; }
    //public List<Client> Clients { get; private set; } = new List<Client>();
    //BankCards items can be added only through AddBankCard method
    private readonly List<BankCard> _bankCards;
    public IReadOnlyCollection<BankCard> BankCards => _bankCards.AsReadOnly(); //Карты

    protected BankAccount()
    {
        _bankCards = new List<BankCard>();
    }

    public BankAccount(Money balance, decimal withdrawalLimit)
    {
        Id = Guid.NewGuid();
        Balance = balance;
        WithdrawalLimit = withdrawalLimit;
    }
    public BankAccount(Guid id, Money balance, decimal withdrawalLimit)
    {
        Id = id;
        Balance = balance;
        WithdrawalLimit = withdrawalLimit;
    }

    public void AddBankCard()
    {
        _bankCards.Add(new BankCard());
    }

    // public void AddClient(Client client)
    // {
    //     Clients.Add(client);
    // }

    public bool IsBalanceNegative() => Balance.Amount < 0;
    public void BalanceIncrease(Money money)
    {
        if (Balance.Currency != money.Currency) throw new FormatException();
        Balance.Increase(money.Amount);
    }

    public void BalanceDecrease(Money money)
    {
        if (Balance.Currency != money.Currency) throw new FormatException();
        if (Balance.Amount < money.Amount) throw new InvalidOperationException();
        Balance.Decrease(money.Amount);
    }
    public void Block()
    {
        if (IsBlocked) throw new InvalidOperationException();
        IsBlocked = true;
    }
    public void Unblock()
    {
        if (!IsBlocked) throw new InvalidOperationException();
        IsBlocked = false;
    }
}