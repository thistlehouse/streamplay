using StreamPlay.Module.Users.Domain;

namespace StreamPlay.Module.Users.Tests.TestUtils.Persistence;

internal class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> GetByEmailAsync(string email)
        => Task.FromResult(_users.FirstOrDefault(u => u.Email == email));

    public Task<User?> GetByIdAsync(Guid userId)
        => Task.FromResult(_users.FirstOrDefault(u => u.Id == userId));
}
