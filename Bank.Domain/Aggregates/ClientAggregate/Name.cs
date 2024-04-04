using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.ClientAggregate;

public class Name : ValueObject
{
    public string FirstName { get; private set; }
    public string? SecondName { get; private set; }
    public string LastName { get; private set; }
    public string Patronymic { get; private set; } //отчество

    public Name(string firstName, string lastName, string patronymic)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }
    public Name(string firstName, string secondName, string lastName, string patronymic)
    {
        FirstName = firstName;
        SecondName = secondName;
        LastName = lastName;
        Patronymic = patronymic;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        if (SecondName != null) yield return SecondName;
        yield return LastName;
        yield return Patronymic;
    }
}