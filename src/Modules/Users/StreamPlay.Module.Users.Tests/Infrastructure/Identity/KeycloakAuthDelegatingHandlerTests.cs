using System.Net;
using System.Text;
using Microsoft.Extensions.Options;
using StreamPlay.Module.Users.Infrastructure.Identity;
using StreamPlay.Module.Users.Tests.TestUtils.Infrastructure.Stubs;

namespace StreamPlay.Module.Users.Tests.Infrastructure.Identity;

public class KeycloakAuthDelegatingHandlerTests
{
    [Fact]
    public async Task KeycloakAuthDelegatingHandler_Should_Add_Bearer_Token_Header()
    {
        var options = Options.Create(new KeycloakOptions
        {
            ConfidentialClientId = "client-id",
            ConfidentialClientSecret = "client-secret",
            TokenUrl = "https://fake-token-endpoint",
        });

        var tokenJson = """{ "access_token": "fake-token" }""";

        var stub = new StubHandler
        {
            Response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(tokenJson, Encoding.UTF8, "application/json"),
            },
        };

        var handler = new KeycloakAuthDelegatingHandler(options)
        {
            InnerHandler = stub,
        };

        var invoker = new HttpMessageInvoker(handler);

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api/protected");
        HttpResponseMessage response = await invoker.SendAsync(request, CancellationToken.None);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(stub.CaptureMessage);

        var authHeader = stub.CaptureMessage!.Headers.Authorization;

        Assert.Equal("Bearer", authHeader!.Scheme);
        Assert.Equal("fake-token", authHeader.Parameter);
    }
}