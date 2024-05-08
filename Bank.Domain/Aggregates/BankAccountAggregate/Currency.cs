using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.BankAccountAggregate;

public class Currency : ValueObject
{
    private readonly string _code;
    public string Code => _code;
    
    public static Currency RUB => new("RUB");
    public static Currency USD => new("USD");
    public static Currency EUR => new("EUR");

    protected Currency()
    {
        
    }
    public Currency(string code)
    {
        if (code is null) throw new ArgumentNullException(nameof(code), "Code can't be null");
        _code = code.ToUpperInvariant();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _code;
    }
}