using Freethings.Offers.Application;
using Freethings.Offers.Domain;
using Freethings.Offers.Infrastructure;
using Freethings.Offers.Presentation;

namespace Freethings.Offers;

public static class DependencyInjection
{
    public static IServiceCollection AddOffers(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDomain()
            .AddApplication()
            .AddInfrastructure(configuration)
            .AddPresentation();
        
        return services;
    }
}