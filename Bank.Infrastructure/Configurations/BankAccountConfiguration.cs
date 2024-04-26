using Bank.Domain.Aggregates.ClientAggregate;
using Bank.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Infrastracture.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> bankAccountConfiguration)
    {
        bankAccountConfiguration.ToTable("BankAccounts");
        bankAccountConfiguration.Ignore(x => x.DomainEvents);
        bankAccountConfiguration.OwnsMany(o => o.BankCards);
    }
}