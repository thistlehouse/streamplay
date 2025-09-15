using StreamPlay.Common.Application.Menssaging;
using StreamPlay.Common.Domain;
using StreamPlay.Module.Users.Application.Authentication.Common;

namespace StreamPlay.Module.Users.Application.Authentication.Registration;

public sealed record RegisterUserCommand(
    string Username,
    string Email,
    string Password) : ICommand<AuthenticationResult>;