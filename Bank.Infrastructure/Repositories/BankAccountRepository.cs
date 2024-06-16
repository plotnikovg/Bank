using Bank.Application.Repositories;
using Bank.Domain.Common;

namespace Bank.Infrastructure.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly BankContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public BankAccountRepository(BankContext context)
    {
        _context = context;
    }
    public BankAccount Add(BankAccount bankAccount)
    {
        return _context.BankAccounts
            .Add(bankAccount)
            .Entity;
    }

    public BankAccount Update(BankAccount bankAccount)
    {
        return _context.BankAccounts
            .Update(bankAccount)
            .Entity;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var bankAccount = await FindByIdAsync(id) ?? throw new ArgumentNullException(nameof(BankAccount), "BankAccount not found");
        var a = _context.BankAccounts
            .Remove(bankAccount);
        return true;
    }

    public async Task<bool> DeleteAsync(BankAccount bankAccount)
    {
        var a =  _context.BankAccounts
            .Remove(bankAccount);
        return true;
    }

    public async Task<BankAccount?> FindByIdAsync(Guid id)
    {
        var bankAccount = await _context.BankAccounts
            .FindAsync(id);
        return bankAccount;
    }
    public async Task<BankAccount?> FindByCardNumberIdAsync(Guid id)
    {
        var bankAccount = await _context.BankAccounts
            .Include(x => x.BankCards)
            .Where(x => x.BankCards.Any(bc => bc.Id == id))
            .FirstOrDefaultAsync();
        return bankAccount;
    }
}