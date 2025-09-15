using StreamPlay.Common.Application.Menssaging;
using StreamPlay.Module.Users.Application.Authentication.Common;

namespace StreamPlay.Module.Users.Application.Authentication.Login;

public sealed record LoginUserQuery(string Email, string Password) : IQuery<AuthenticationResult>;