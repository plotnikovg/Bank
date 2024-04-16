using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.BankAccountAggregate;

public class BankAccount : BaseEntity, IAggregateRoot
{
    public Money Balance { get; private set; }
    public decimal WithdrawalLimit { get; private set; } //Лимит на снятие средств
    public bool IsLocked { get; private set; }
    //BankCards items can be added only through AddBankCard method
    private readonly List<BankCard> _bankCards;
    public IReadOnlyCollection<BankCard> BankCards => _bankCards.AsReadOnly(); //Карты

    protected BankAccount()
    {
        _bankCards = new List<BankCard>();
    }

public BankAccount(Money balance, decimal withdrawalLimit)
    {
        Balance = balance;
        WithdrawalLimit = withdrawalLimit;
    }

    public void AddBankCard()
    {
        _bankCards.Add(new BankCard());
    }

    public bool IsBalanceNegative() => Balance.Amount < 0;
    public void BalanceIncrease(Money money)
    {
        if (Balance.Currency != money.Currency) throw new InvalidOperationException();
        Balance.Increase(money.Amount);
    }

    public void BalanceDecrease(Money money)
    {
        if (Balance.Currency != money.Currency) throw new InvalidOperationException();
        Balance.Decrease(money.Amount);
    }
}