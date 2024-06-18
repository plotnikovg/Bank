using Bank.Domain.Aggregates.BankAccountAggregate;
using Bank.Domain.Common;

namespace Bank.Application.Repositories;

public interface IClientRepository
{
    IUnitOfWork UnitOfWork { get; }
    Client Add(Client client);
    Client Update(Client client);
    Task<Client?> FindByPhoneNumberAsync(string phoneNumber);
    Task<Client?> FindByPassportAsync(Passport passport);
    Task<Client> FindByIdAsync(Guid id);
    Task<Client> FindByUserIdAsync(string userId);
    Client AddBankAccount(Client client, BankAccount bankAccount);
    Client AddBankAccount(Guid clientId, BankAccount bankAccount);
    Client AddBankAccount(Passport clientPassport, BankAccount bankAccount);
}