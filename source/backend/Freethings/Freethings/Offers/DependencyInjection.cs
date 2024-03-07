using Freethings.Offers.Application;
using Freethings.Offers.Domain;
using Freethings.Offers.Infrastructure;
using Freethings.Offers.Presentation;
using Freethings.Offers.Presentation.AddOffer;

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
    
    public static WebApplication MapOffersEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("api/offers");
        
        group.MapAddOfferEndpoint();
        
        return app;
    }
}