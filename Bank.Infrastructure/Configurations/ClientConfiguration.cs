namespace Bank.Infrastructure.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> clientConfiguration)
    {
        clientConfiguration.ToTable("Clients");
        clientConfiguration.Ignore(x => x.DomainEvents);
        clientConfiguration.Ignore(p => p.Name);
        // clientConfiguration.OwnsOne(p => p.Passport).OwnsOne(p => p.Name);
        // clientConfiguration.OwnsOne(p => p.Passport).OwnsOne(p => p.RegistrationAddress);
        // clientConfiguration.OwnsOne(p => p.Passport, a =>
        // {
        //     a.OwnsOne(o => o.Name);
        //     a.OwnsOne(o => o.RegistrationAddress);
        // });
        clientConfiguration.OwnsOne(p => p.Passport, n =>
        {
            n.OwnsOne(o => o.Name);
            n.OwnsOne(o => o.RegistrationAddress);
        });
        

        //clientConfiguration.OwnsMany() TODO
    }
}