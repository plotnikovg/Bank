namespace Bank.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BankContext _context;

    public UserRepository(BankContext context)
    {
        _context = context;
    }

    public User Add(User user)
    {
        return _context.Users
            .Add(user)
            .Entity;
    }

    public User Update(User user)
    {
        return _context.Users
            .Update(user)
            .Entity;
    }

    public async Task<User?> FindAsync(string login)
    {
        var user = await _context.Users
            .Where(p => p.Login == login)
            .FirstOrDefaultAsync();
        return user;
    }

    public async Task<User> FindByIdAsync(Guid id)
    {
        var user = await _context.Users
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        return user;
    }
    
    
}