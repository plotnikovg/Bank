using Bank.Domain.Aggregates.ClientAggregate;
using Bank.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastracture;

public class BankContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<User> Users { get; set; }
    
    public BankContext(DbContextOptions<BankContext> options) : base(options) { }
}