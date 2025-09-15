using Microsoft.Extensions.Logging;
using StreamPlay.Common.Domain;
using StreamPlay.Module.Users.Application.Abstractions.Identity;

namespace StreamPlay.Module.Users.Infrastructure.Identity;

public sealed class IdentityProviderService(
    KeycloakClient keycloakClient,
    ILogger<IdentityProviderService> logger) : IIdentityProviderService
{
    private const string PasswordCredentialType = "password";

    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Username,
            user.Email,
            true,
            true,
            [new CredentialRepresentation(PasswordCredentialType, user.Password, false)]);

        try
        {
            string identityId = await keycloakClient.RegisterUserAsync(userRepresentation, cancellationToken);
            return identityId;
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "User registration failed");

            return Result.Failure<string>(Error.Problem("Error.Error", ex.Message));
        }
    }
}
