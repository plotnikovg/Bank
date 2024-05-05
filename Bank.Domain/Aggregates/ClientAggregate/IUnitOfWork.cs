namespace Bank.Domain.Aggregates.ClientAggregate;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}