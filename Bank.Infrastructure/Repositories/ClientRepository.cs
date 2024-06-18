using Bank.Application.Repositories;
using Bank.Domain.Common;

namespace Bank.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly BankContext _context;
    public IUnitOfWork UnitOfWork => _context;

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
    
    public async Task<Client?> FindByPhoneNumberAsync(string phoneNumber)
    {
        var client = await _context.Clients
            .Where(p => p.PhoneNumber == phoneNumber)
            .FirstOrDefaultAsync();
        return client;
    }

    public async Task<Client?> FindByPassportAsync(Passport passport)
    {
        var client = await _context.Clients
            .Where(p => p.Passport == passport)
            .FirstOrDefaultAsync();
        return client;
    }

    public async Task<Client> FindByIdAsync(Guid id)
    {
        var client = await _context.Clients
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        return client;
    }

    public async Task<Client> FindByUserIdAsync(string userId)
    {
        var client = await _context.Clients
            .Where(p => p.UserId == userId)
            .FirstOrDefaultAsync();
        return client;
    }

    public Client AddBankAccount(Client client, BankAccount bankAccount)
    {
        client.AddBankAccount(bankAccount);
        return this.Update(client);
    }

    public Client AddBankAccount(Guid clientId, BankAccount bankAccount)
    {
        var client = this.FindByIdAsync(clientId).Result;
        return this.AddBankAccount(client, bankAccount);
    }

    public Client AddBankAccount(Passport clientPassport, BankAccount bankAccount)
    {
        var client = this.FindByPassportAsync(clientPassport).Result ?? throw new ArgumentNullException(nameof(clientPassport));
        return this.AddBankAccount(client, bankAccount);
    }
}