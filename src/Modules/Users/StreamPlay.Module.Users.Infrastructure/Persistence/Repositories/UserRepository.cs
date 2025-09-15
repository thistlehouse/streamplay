using StreamPlay.Module.Users.Domain;

namespace StreamPlay.Module.Users.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = [];

    public Task AddAsync(User user)
    {
        _users.Add(user);

        return Task.CompletedTask;
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        var user = _users.Find(user => user.Email == email);

        return Task.FromResult(user);

    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        var user = _users.Find(user => user.Id == id);

        return Task.FromResult(user);
    }
}