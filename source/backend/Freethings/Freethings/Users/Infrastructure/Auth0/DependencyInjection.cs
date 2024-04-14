using Freethings.Users.Application.Services;

namespace Freethings.Users.Infrastructure.Auth0;

internal static class DependencyInjection
{
    public static IServiceCollection AddAuth0ManagementApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Auth0Options>(configuration.GetSection(Auth0Options.SectionName));
        
        services.AddScoped<IIdentityProviderService, Auth0ManagementClientService>();

        return services;
    }
}