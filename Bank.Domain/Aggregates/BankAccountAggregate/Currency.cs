using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.BankAccountAggregate;

public class Currency : ValueObject
{
    private string _code;

    public string Code
    {
        get { return _code; } 
        set { _code = value; }
    }
    
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