namespace Bank.Infrastructure.Configurations;

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