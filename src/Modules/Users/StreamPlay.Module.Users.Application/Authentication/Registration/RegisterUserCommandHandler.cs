using StreamPlay.Common.Application.Menssaging;
using StreamPlay.Common.Domain;
using StreamPlay.Module.Users.Application.Abstractions.Identity;
using StreamPlay.Module.Users.Application.Authentication.Common;
using StreamPlay.Module.Users.Domain;

namespace StreamPlay.Module.Users.Application.Authentication.Registration;

public sealed class RegisterUserCommandHandler(
    IIdentityProviderService identityProviderService,
    IUserRepository userRepository) : ICommandHandler<RegisterUserCommand, AuthenticationResult>
{
    public async Task<Result<AuthenticationResult>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userModel = new UserModel(request.Username, request.Email, request.Password);
        Result<string> result = await identityProviderService.RegisterUserAsync(userModel, cancellationToken);
        if (result.IsFailure)
        {
            return Result.Failure<AuthenticationResult>(result.Error);
        }

        User? user = User.Create(request.Username, request.Email, request.Password);
        //TODO: hash user's password

        await userRepository.AddAsync(user);
        return new AuthenticationResult(user, "mytoken");
    }
}