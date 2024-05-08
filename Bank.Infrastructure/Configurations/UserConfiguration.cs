namespace Bank.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> userConfiguration)
    {
        userConfiguration.ToTable("Users");
        userConfiguration.Ignore(x => x.DomainEvents);
        userConfiguration.Property(o => o.Login).IsRequired();
        userConfiguration.Property(o => o.Password).IsRequired();
        userConfiguration.OwnsOne(p => p.Role, a =>
        {
            a.Property(u => u.Value).HasColumnName("Role");
            a.Property(u => u.Value).HasColumnType("nvarchar(20)");
        });
    }
}