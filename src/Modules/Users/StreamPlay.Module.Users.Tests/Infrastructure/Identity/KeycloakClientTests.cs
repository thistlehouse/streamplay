using System.Net;
using NSubstitute;
using StreamPlay.Module.Users.Infrastructure.Identity;

namespace StreamPlay.Module.Users.Tests.Infrastructure.Identity;

public class KeycloakClientTests
{
    [Fact]
    public async Task KeycloakClient_Should_ReturnIdentityIdAsync()
    {
        var identityIdExpected = "12345";
        var user = new UserRepresentation(
            "UsernameTest",
            "username@email.com",
            true,
            true,
            [new CredentialRepresentation("password", "mypassword", false)]);

        var httpClient = new HttpClient(new FakeHandler())
        {
            BaseAddress = new Uri("http://localhost:8080"),
        };

        var keycloakClient = new KeycloakClient(httpClient);
        var userIdentityId = await keycloakClient.RegisterUserAsync(user);

        Assert.Equal(identityIdExpected, userIdentityId);
    }

    private class FakeHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Headers = { Location = new Uri("http://localhost:8080/users/12345") }
            };

            return Task.FromResult(response);
        }
    }
}