using StreamPlay.Common.Application.Menssaging;
using StreamPlay.Common.Domain;
using StreamPlay.Module.Users.Application.Authentication.Common;
using StreamPlay.Module.Users.Domain;

namespace StreamPlay.Module.Users.Application.Authentication.Login;

public sealed class LoginUserQueryHandler(IUserRepository userRepository)
    : IQueryHandler<LoginUserQuery, AuthenticationResult>
{
    public async Task<Result<AuthenticationResult>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        if (await userRepository.GetByEmailAsync(request.Email) is not User user)
        {
            return Result.Failure<AuthenticationResult>(Error.NotFound(
                "User.NotFound",
                "User was not found"));
        }

        if (user.Password != request.Password)
        {
            return Result.Failure<AuthenticationResult>(Error.Validation(
                "Auth.InvalidCredentials",
                "Invalid credentials"));
        }

        string token = "mytoken";

        return new AuthenticationResult(user, token);
    }
}