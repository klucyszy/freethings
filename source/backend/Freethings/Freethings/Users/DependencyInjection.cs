using Freethings.Users.Infrastructure;
using Freethings.Users.Presentation;

namespace Freethings.Users;

internal static class DependencyInjection
{
    public static IServiceCollection AddUsers(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPresentation()
            .AddInfrastructure(configuration);
        
        return services;
    }
}