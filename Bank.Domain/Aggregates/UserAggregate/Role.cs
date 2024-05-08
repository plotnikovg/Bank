using System.ComponentModel.DataAnnotations.Schema;
using Bank.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Bank.Domain.Aggregates.UserAggregate;

[Keyless]
[NotMapped]
public class Role : ValueObject
{
    private string _value;
    public string Value => _value;
    
    public static Role Admin => new("Admin");
    public static Role Manager => new("Manager");
    public static Role Client => new("Client");

    protected Role()
    {
        
    }
    private Role(string value)
    {
        _value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
}