using Freethings.Auctions.Presentation;

namespace Freethings.Auctions;

public static class DependencyInjection
{
    public static IServiceCollection AddAuctions(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPresentation();
        
        return services;
    }
}