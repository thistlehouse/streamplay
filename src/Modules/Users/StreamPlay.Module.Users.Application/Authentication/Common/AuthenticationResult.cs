using StreamPlay.Module.Users.Domain;

namespace StreamPlay.Module.Users.Application.Authentication.Common;

public sealed record AuthenticationResult(User User, string Token);