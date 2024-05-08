namespace Bank.Infrastructure.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> clientConfiguration)
    {
        clientConfiguration.ToTable("Clients");
        clientConfiguration.Ignore(x => x.DomainEvents);
        clientConfiguration.Ignore(p => p.Name);
        clientConfiguration.OwnsOne(p => p.Passport).OwnsOne(p => p.Name);
        clientConfiguration.OwnsOne(p => p.Passport).OwnsOne(p => p.RegistrationAddress);
            
        //clientConfiguration.OwnsMany() TODO
    }
}