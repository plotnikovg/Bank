using Bank.Domain.Aggregates.BankAccountAggregate;
using Bank.Domain.Common;

namespace Bank.Infrastructure;

public class BankContext : DbContext, IUnitOfWork
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<User> Users { get; set; }
    
    public BankContext(DbContextOptions<BankContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO
        modelBuilder.Entity<Client>().HasData(
            new Client(new Passport(new Name("Иванов", "Сергей", "Михайлович"), "1234", "22226666",
                    new RegistrationAddress(new DateOnly(2003, 6, 18), "-", "Москва", "Комсомольская", 27)),
                "+7(999)999-99-99"));
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return true;
    }
}