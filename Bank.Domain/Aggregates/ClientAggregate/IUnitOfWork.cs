namespace Bank.Domain.Aggregates.ClientAggregate;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}