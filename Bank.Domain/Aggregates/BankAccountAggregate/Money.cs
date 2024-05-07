using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.BankAccountAggregate;

public class Money : ValueObject
{
    public Currency Currency { get; private set; }
    public decimal Amount { get; private set; }

    public Money(Currency currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }
    public Money(string code, decimal amount)
    {
        Currency = new Currency(code);
        Amount = amount;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Currency;
        yield return Amount;
    }
    public void Increase(decimal amount) => Amount += amount;
    public void Decrease(decimal amount) => Amount -= amount;
    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency) throw new InvalidOperationException();
        return new Money(left.Currency, left.Amount + right.Amount);
    }
    public static Money operator -(Money left, Money right)
    {
        if (left.Currency != right.Currency) throw new InvalidOperationException();
        return new Money(left.Currency, left.Amount - right.Amount);
    }

    public static bool operator <(Money left, Money right)
    {
        if (left.Currency != right.Currency) throw new InvalidOperationException();
        return left.Amount < right.Amount;
    }
    public static bool operator >(Money left, Money right)
    {
        if (left.Currency != right.Currency) throw new InvalidOperationException();
        return left.Amount > right.Amount;
    }
}