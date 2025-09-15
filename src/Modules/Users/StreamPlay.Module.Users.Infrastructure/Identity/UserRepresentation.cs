namespace StreamPlay.Module.Users.Infrastructure.Identity;

public sealed record UserRepresentation(
    string Username,
    string Email,
    bool EmailVerified,
    bool Enabled,
    CredentialRepresentation[] Credentials);
