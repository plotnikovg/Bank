namespace Bank.Domain.Aggregates.ClientAggregate;

public interface IClientRepository
{
    Client Add(Client client);
    Client Update(Client client);
    Task<Client?> FindByPhoneNumberAsync(string login);
    Task<Client?> FindByPassportAsync(Passport passport);
    Task<Client> FindByIdAsync(Guid id);
}