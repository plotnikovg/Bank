using Bank.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Infrastracture.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> userConfiguration)
    {
        userConfiguration.ToTable("Users");
        userConfiguration.Ignore(x => x.DomainEvents);
        userConfiguration.Property(o => o.Login).IsRequired();
        userConfiguration.Property(o => o.Password).IsRequired();
        userConfiguration.OwnsOne(p => p.Role);
    }
}