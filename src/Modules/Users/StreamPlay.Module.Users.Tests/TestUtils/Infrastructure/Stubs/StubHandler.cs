using System.Net;

namespace StreamPlay.Module.Users.Tests.TestUtils.Infrastructure.Stubs;

public class StubHandler : HttpMessageHandler
{
    public HttpRequestMessage? CaptureMessage { get; private set; }
    public HttpResponseMessage Response { get; set; } = new HttpResponseMessage(HttpStatusCode.OK);

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        CaptureMessage = request;
        return Task.FromResult(Response);
    }
}