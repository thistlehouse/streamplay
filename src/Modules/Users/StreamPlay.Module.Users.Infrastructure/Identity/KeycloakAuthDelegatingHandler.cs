using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace StreamPlay.Module.Users.Infrastructure.Identity;

public sealed class KeycloakAuthDelegatingHandler(IOptions<KeycloakOptions> options) : DelegatingHandler
{
    private readonly KeycloakOptions _options = options.Value;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        AuthToken authToken = await GetAuthorizationToken(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken.AccessToken);
        HttpResponseMessage httpResponse = await base.SendAsync(request, cancellationToken);

        httpResponse.EnsureSuccessStatusCode();

        return httpResponse;
    }

    private async Task<AuthToken> GetAuthorizationToken(CancellationToken cancellationToken)
    {
        var authRequestParams = new KeyValuePair<string, string>[]
        {
            new("client_id", _options.ConfidentialClientId),
            new("client_secret", _options.ConfidentialClientSecret),
            new("scope", "openid"),
            new("grant_type", "client_credentials"),
        };

        using var authRequestContent = new FormUrlEncodedContent(authRequestParams);
        using var authRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(_options.TokenUrl));
        authRequest.Content = authRequestContent;
        using HttpResponseMessage authorizationResponse = await base.SendAsync(authRequest, cancellationToken);

        authorizationResponse.EnsureSuccessStatusCode();

        var response = await authorizationResponse.Content.ReadFromJsonAsync<AuthToken>(cancellationToken);
        return response!;
    }

    public sealed class AuthToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; }
    }
}