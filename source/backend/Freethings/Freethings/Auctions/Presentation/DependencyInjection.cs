using Freethings.Auctions.Presentation.Endpoints;

namespace Freethings.Auctions.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        return services;
    }
    
    public static WebApplication MapAuctionsEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("api/users/{userId:guid}/auctions/{auctionId:guid}");
        
        group.MapClaimItemEndpoint();

        group.WithName("Auctions");
        
        return app;
    }
}