using Freethings.Auctions.Infrastructure;
using Freethings.Auctions.Presentation;

namespace Freethings.Auctions;

internal static class DependencyInjection
{
    public static IServiceCollection AddAuctions(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPresentation()
            .AddInfrastructure(configuration);
        
        return services;
    }
}