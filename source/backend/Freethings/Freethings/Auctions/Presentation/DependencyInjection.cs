using Freethings.Auctions.Presentation.Endpoints;
using Freethings.Shared.Infrastructure.Api.Filters;

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
            .MapGroup("api/auctions")
            .MapCreateAuctionAdvertEndpoint()
            .MapClaimAuctionItemEndpoint()
            .MapReserveAuctionItemEndpoint()
            .MapGetAuctionsEndpoint()
            .MapGetAuctionEndpoint()
            .MapEditAuctionAdvertMetadataEndpoint()
            .MapChangeAvailableQuantityEndpoint()
            .WithTags("Auctions")
            .RequireInputValidation()
            .RequireAuthorization();
        
        return app;
    }
}