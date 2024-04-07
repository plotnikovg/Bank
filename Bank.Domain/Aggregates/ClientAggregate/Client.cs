using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.ClientAggregate;

//Bank client
public class Client : BaseEntity, IAggregateRoot
{
    public Name Name { get; private set; }
    
}