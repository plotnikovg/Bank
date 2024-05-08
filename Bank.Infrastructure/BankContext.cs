using Bank.Domain.Common;
using Bank.Infrastructure.Configurations;

namespace Bank.Infrastructure;

public class BankContext : DbContext, IUnitOfWork
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<User> Users { get; set; }
    
    private readonly IMediator _mediator;

    public BankContext()
    { }
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    { }
    // public BankContext(DbContextOptions<BankContext> options, IMediator mediator) : base(options)
    // {
    //     _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    //     
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
        // modelBuilder.Entity<Client>().HasData(
        //     new Client(new Passport(new Name("Сергей","Михаил", "Иванов", "Михайлович"), "1234", "22226666",
        //             new RegistrationAddress(new DateOnly(2003, 6, 18), "-", "Москва", "Комсомольская", 27)),
        //         "+7(999)999-99-99"));
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return true;
    }
}