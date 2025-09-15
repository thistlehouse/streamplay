using StreamPlay.Common.Domain;
using StreamPlay.Module.Users.Domain;

namespace StreamPlay.Module.Users.Application.Abstractions.Identity;

public interface IIdentityProviderService
{
    Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default);
}