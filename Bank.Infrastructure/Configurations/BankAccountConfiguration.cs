using Bank.Domain.Aggregates.BankAccountAggregate;

namespace Bank.Infrastructure.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> bankAccountConfiguration)
    {
        bankAccountConfiguration.ToTable("BankAccounts");
        bankAccountConfiguration.Ignore(x => x.DomainEvents);
        bankAccountConfiguration.OwnsOne(o => o.Balance)
            .OwnsOne(o => o.Currency);
        bankAccountConfiguration.OwnsMany(o => o.BankCards)
            .Ignore(x => x.DomainEvents);
    }
}