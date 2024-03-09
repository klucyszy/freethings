using Freethings.Offers.Presentation.AddOffer;
using Freethings.Offers.Presentation.EditOffer;
using Freethings.Offers.Presentation.GetOffer;
using Freethings.Offers.Presentation.GetOffers;
using Freethings.Offers.Presentation.RemoveOffer;

namespace Freethings.Offers.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        return services;
    }
    
    public static WebApplication MapOffersEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("api/users/{userId:guid}/offers");
        
        group.MapAddOfferEndpoint();
        group.MapGetOffersEndpoint();
        group.MapGetOfferEndpoint();
        group.MapEditOfferEndpoint();
        group.MapRemoveOfferEndpoint();
        
        return app;
    }
}