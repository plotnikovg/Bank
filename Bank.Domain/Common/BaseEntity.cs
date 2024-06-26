namespace Bank.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; private protected set; }

    private readonly List<BaseEvent> _domainEvents = [];
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }
    public void ClearDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Clear();
    }
}