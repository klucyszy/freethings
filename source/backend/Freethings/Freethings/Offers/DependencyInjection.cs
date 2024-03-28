using Freethings.Offers.Application;
using Freethings.Offers.Infrastructure;
using Freethings.Offers.Presentation;

namespace Freethings.Offers;

internal static class DependencyInjection
{
    public static IServiceCollection AddOffers(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApplication()
            .AddInfrastructure(configuration)
            .AddPresentation();
        
        return services;
    }
}