using Bank.Infrastructure.Identity;
using Bank.Domain.Common;
using Bank.Infrastructure.Configurations;


namespace Bank.Infrastructure;

public class BankContext : IdentityDbContext<IdentityUser>, IUnitOfWork
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public new DbSet<User> Users { get; set; }
    
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
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
        // modelBuilder.Entity<Client>()
        //     .HasData(
        //         new Client(
        //             new Passport(
        //                 new Name("Иван", "Петрович", "Иванов", "Иванович"),
        //                 "1234",
        //                 "567890",
        //                 new RegistrationAddress(
        //                     new DateOnly(2000, 1, 1),
        //                     "Москва",
        //                     "Москва",
        //                     "Улица",
        //                     1,
        //                     "2")), 
        //             "+7(999)999-99-99")
        //     );
        // modelBuilder.Entity<Client>().HasData(
        //     new {Id = "9e677d52-8a3f-4bb7-b488-b0c8ccf644fa", PhoneNumber = "+7(999)999-99-99"});
        // modelBuilder.Entity<Client>().OwnsOne(p => p.Passport).HasData(
        //     new {ClientId = "9e677d52-8a3f-4bb7-b488-b0c8ccf644fa", Series = "1234", Number = "567890" });
        // modelBuilder.Entity<Client>().OwnsOne(p => p.Passport).OwnsOne(n => n.Name).HasData(
        //     new
        //     {
        //         ClientId = "9e677d52-8a3f-4bb7-b488-b0c8ccf644fa", FirstName = "Иван", SecondName = "Петрович",
        //         LastName = "Иванов", MiddleName = "Иванович"
        //     }
        // );
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return true;
    }
}