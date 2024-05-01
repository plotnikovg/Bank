namespace Bank.Infrastracture.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly BankContext _context;

    public ClientRepository(BankContext context)
    {
        _context = context;
    }
    public Client Add(Client client)
    {
        return _context.Clients
            .Add(client)
            .Entity;
    }

    public Client Update(Client client)
    {
        return _context.Clients
            .Update(client)
            .Entity;
    }

    public Task<Client?> FindAsync(Passport passport)
    {
        var client = _context.Clients
            .Where(p => p.Passport == passport)
            .FirstOrDefaultAsync();
        return client;
    }

    public Task<Client> FindByIdAsync(Guid id)
    {
        var client = _context.Clients
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        return client;
    }
}