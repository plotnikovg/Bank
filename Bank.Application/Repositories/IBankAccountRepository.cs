using Bank.Domain.Aggregates.BankAccountAggregate;
using Bank.Domain.Common;

namespace Bank.Application.Repositories;

public interface IBankAccountRepository
{
    IUnitOfWork UnitOfWork { get; }
    BankAccount Add(BankAccount bankAccount);
    BankAccount Update(BankAccount bankAccount);
    Task<bool> DeleteByIdAsync(Guid id);
    Task<bool> DeleteAsync(BankAccount bankAccount);
    Task<BankAccount?> FindByIdAsync(Guid id);
}