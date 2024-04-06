using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.ClientAggregate;

//Bank client
public class Client : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Name Name { get; private set; }
    
}