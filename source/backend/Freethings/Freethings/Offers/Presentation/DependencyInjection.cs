using Freethings.Offers.Presentation.Endpoints;

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
        //group.MapPublishOfferEndpoint();
        group.MapGetOffersEndpoint();
        group.MapGetOfferEndpoint();
        group.MapEditOfferEndpoint();
        group.MapRemoveOfferEndpoint();
        
        group.WithName("Offers");
        
        return app;
    }
}