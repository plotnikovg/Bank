namespace Bank.Domain.Aggregates.ClientAggregate;

public interface IClientRepository
{
    Client Add(Client client);
    Client Update(Client client);
    Task<Client> FindAsync(Passport passport);
    Task<Client> FindByIdAsync(Guid id);
}