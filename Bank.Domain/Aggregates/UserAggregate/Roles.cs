using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.UserAggregate;

public class Roles : ValueObject
{
    public static Roles Admin => new("Admin");
    public static Roles Manager => new("Manager");
    public static Roles Client => new("Client");

    private string _value;
    public string Value => _value;

    protected Roles()
    {
        
    }
    private Roles(string value)
    {
        _value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
}