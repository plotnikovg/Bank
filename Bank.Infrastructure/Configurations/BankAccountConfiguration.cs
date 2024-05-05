namespace Bank.Infrastructure.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> bankAccountConfiguration)
    {
        bankAccountConfiguration.ToTable("BankAccounts");
        bankAccountConfiguration.Ignore(x => x.DomainEvents);
        bankAccountConfiguration.OwnsMany(o => o.BankCards);
    }
}