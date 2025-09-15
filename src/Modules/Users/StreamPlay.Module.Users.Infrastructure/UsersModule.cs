using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StreamPlay.Common.Presentation.Endpoints;
using StreamPlay.Module.Users.Application.Abstractions.Identity;
using StreamPlay.Module.Users.Domain;
using StreamPlay.Module.Users.Infrastructure.Identity;
using StreamPlay.Module.Users.Infrastructure.Persistence.Repositories;

namespace StreamPlay.Module.Users.Infrastructure;

public static class UsersModule
{
    public static IServiceCollection AddUsersModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddEndpoint(Presentation.AssemblyReference.Assembly);

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<KeycloakOptions>(configuration.GetSection("Users:Keycloak"));
        services.AddTransient<KeycloakAuthDelegatingHandler>();
        services.AddTransient<IIdentityProviderService, IdentityProviderService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddHttpClient<KeycloakClient>((serviceProvider, httpClient) =>
        {
            KeycloakOptions keycloakOptions = serviceProvider
                .GetRequiredService<IOptions<KeycloakOptions>>()
                .Value;

            httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
        })
        .AddHttpMessageHandler<KeycloakAuthDelegatingHandler>();


        return services;
    }
}