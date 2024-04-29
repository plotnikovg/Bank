namespace Bank.Domain.Aggregates.UserAggregate;

public interface IUserRepository
{
    User Add(User user);
    User Update(User user);
    Task<User> FindAsync(string login);
    Task<User> FindByIdAsync(Guid id);
}