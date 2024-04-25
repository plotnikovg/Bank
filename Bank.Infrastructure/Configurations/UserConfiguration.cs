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
        userConfiguration.OwnsOne(p => p.Role);
    }
}