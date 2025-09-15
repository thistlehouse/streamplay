using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StreamPlay.Common.Domain;
using StreamPlay.Common.Presentation.Endpoints;
using StreamPlay.Common.Presentation.Results;
using StreamPlay.Module.Users.Application.Authentication.Common;
using StreamPlay.Module.Users.Application.Authentication.Registration;

namespace StreamPlay.Users.Presentation.Users;

internal sealed class RegisterUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "user/register",
            async (Request request, ISender sender) =>
            {
                Result<AuthenticationResult> result = await sender.Send(
                    new RegisterUserCommand(
                        request.Username,
                        request.Email,
                        request.Password));

                return result.Match(Results.Ok, ApiResult.Problem);
            })
            .AllowAnonymous()
            .WithTags(Tags.Users);
    }

    internal sealed class Request
    {
        public string Email { get; init; }

        public string Password { get; init; }

        public string Username { get; init; }

    }
}