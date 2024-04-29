using Bank.Domain.Aggregates.ClientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Infrastracture.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> clientConfiguration)
    {
        clientConfiguration.ToTable("Clients");
        clientConfiguration.Ignore(x => x.DomainEvents);
        clientConfiguration.OwnsOne(p => p.Passport)
            .Ignore(x => x.Name);
        //clientConfiguration.OwnsMany() TODO
    }
}