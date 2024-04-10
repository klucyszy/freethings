using Auth0.ManagementApi;
using Freethings.Shared.Infrastructure.Options;
using Freethings.Users.Application.Services;

namespace Freethings.Users.Infrastructure.Auth0;

internal static class DependencyInjection
{
    public static IServiceCollection AddAuth0ManagementApi(this IServiceCollection services, IConfiguration configuration)
    {
        Auth0Options auth0Options = configuration.GetOptions<Auth0Options>(Auth0Options.SectionName);
        
        services.AddSingleton(_ => new ManagementApiClient(
            configuration["Auth0:ManagementApiToken"],
            auth0Options.BaseUrl));
        
        // services.AddHttpClient<IIdentityProviderService, Auth0ManagementClientService>(client =>
        // {
        //     client.BaseAddress = new Uri(auth0Options.BaseUrl);
        //     client.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration["Auth0:ManagementApiToken"]}");
        // });

        services.AddScoped<IIdentityProviderService, Auth0ManagementClientService>();

        return services;
    }
}