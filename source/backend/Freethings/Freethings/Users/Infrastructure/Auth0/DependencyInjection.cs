using Freethings.Shared.Infrastructure.Options;
using Freethings.Users.Application.Services;

namespace Freethings.Users.Infrastructure.Auth0;

public static class DependencyInjection
{
    public static IServiceCollection AddAuth0(this IServiceCollection services, IConfiguration configuration)
    {
        Auth0Options auth0Options = configuration.GetOptions<Auth0Options>(Auth0Options.SectionName);
        
        services.AddHttpClient<IIdentityProviderSerivce, Auth0ManagementClientService>(client =>
        {
            client.BaseAddress = new Uri(auth0Options.BaseUrl);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration["Auth0:ManagementApiToken"]}");
        });

        services.AddScoped<IIdentityProviderSerivce, Auth0ManagementClientService>();

        return services;
    }
}