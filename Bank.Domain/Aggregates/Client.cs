using Bank.Domain.Common;

namespace Bank.Domain.Aggregates;

//Bank client
public class Client : IAggregateRoot
{
    public Guid Id { get; private set; }
}