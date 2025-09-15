using Microsoft.AspNetCore.Routing;

namespace StreamPlay.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
