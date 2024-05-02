using Bank.Domain.Aggregates.UserAggregate;

namespace Bank.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Add(User user)
    {
        return _userRepository.Add(user);
    }

    public User Update(User user)
    {
        return _userRepository.Update(user);
    }

    public async Task<User?> FindAsync(string login)
    {
        return await _userRepository.FindAsync(login);
    }

    public async Task<User> FindByIdAsync(Guid id)
    {
        return await _userRepository.FindByIdAsync(id);
    }
}