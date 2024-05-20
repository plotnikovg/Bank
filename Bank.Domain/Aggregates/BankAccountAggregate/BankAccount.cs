using Bank.Domain.Aggregates.ClientAggregate;
using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.BankAccountAggregate;

public class BankAccount : BaseEntity
{
    public Money Balance { get; private set; }
    //Лимит на снятие средств
    public decimal WithdrawalLimit { get; private set; } 
    public bool IsBlocked { get; private set; }
    //Банковские карты могут быть добавлены
    //только черещ метод AddBankCard()
    private readonly List<BankCard> _bankCards;
    public IReadOnlyCollection<BankCard> BankCards => _bankCards.AsReadOnly();

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
    public void AddBankCard()
    {
        _bankCards.Add(new BankCard());
    }
    public bool IsBalanceNegative() => Balance.Amount < 0;
    //Заблокировать счёт
    public void Block()
    {
        if (IsBlocked) throw new InvalidOperationException();
        IsBlocked = true;
    }
    //Разблокировать счёт
    public void Unblock()
    {
        if (!IsBlocked) throw new InvalidOperationException();
        IsBlocked = false;
    }
}