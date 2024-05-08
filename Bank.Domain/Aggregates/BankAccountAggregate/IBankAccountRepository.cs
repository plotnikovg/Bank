using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.BankAccountAggregate;

public interface IBankAccountRepository
{
    IUnitOfWork UnitOfWork { get; }
    BankAccount Add(BankAccount bankAccount);
    BankAccount Update(BankAccount bankAccount);
    Task<bool> DeleteByIdAsync(Guid id);
    Task<bool> DeleteAsync(BankAccount bankAccount);
    Task<BankAccount?> FindByIdAsync(Guid id);
}