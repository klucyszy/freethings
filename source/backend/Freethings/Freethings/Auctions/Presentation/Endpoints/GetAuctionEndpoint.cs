using Freethings.Auctions.Infrastructure.Queries;
using Freethings.Auctions.Infrastructure.Queries.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class GetAuctionEndpoint
{
    public static RouteGroupBuilder MapGetAuctionEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{auctionId:guid}", async Task<Results<Ok<AuctionDto>, NotFound>> (
            [FromRoute] Guid userId,
            [FromRoute] Guid auctionId,
            ISender sender,
            CancellationToken ct) =>
        {
            AuctionDto result = await sender.Send(new GetAuctionQuery(
                userId,
                auctionId
            ), ct);

            return result is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(result);
        })
        .RequireAuthorization();

        return group;
    }
}