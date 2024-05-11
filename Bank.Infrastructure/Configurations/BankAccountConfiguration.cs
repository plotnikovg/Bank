using Bank.Domain.Aggregates.BankAccountAggregate;

namespace Bank.Infrastructure.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> bankAccountConfiguration)
    {
        bankAccountConfiguration.ToTable("BankAccounts");
        bankAccountConfiguration.Ignore(x => x.DomainEvents);
        bankAccountConfiguration.OwnsOne(p => p.Balance, n => 
        { 
            n.OwnsOne(o => o.Currency, c =>
            {
                c.Property(x => x.Code).HasMaxLength(4);
            });
        });
        bankAccountConfiguration.OwnsMany(o => o.BankCards)
            .Ignore(x => x.DomainEvents);
    }
}