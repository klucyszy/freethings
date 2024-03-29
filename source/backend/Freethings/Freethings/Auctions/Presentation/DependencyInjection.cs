using Freethings.Auctions.Presentation.Endpoints;

namespace Freethings.Auctions.Presentation;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        return services;
    }
    
    public static WebApplication MapAuctionsEndpoints(this WebApplication app)
    {
        app
            .MapGroup("api/users/{userId:guid}/auctions/{auctionId:guid}")
            .MapAuctionEndpoints()
            .WithTags("Auctions");
        
        return app;
    }
}