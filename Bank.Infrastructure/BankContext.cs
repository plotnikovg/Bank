using Bank.Domain.Aggregates.ClientAggregate;
using Bank.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastracture;

public class BankContext : DbContext, IUnitOfWork
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<User> Users { get; set; }
    
    public BankContext(DbContextOptions<BankContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO
    }

    public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}