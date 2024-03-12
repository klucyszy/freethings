using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class ClaimAuctionItemEndpoint
{
    public static void MapClaimItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("claim", async (
            [FromRoute] Guid userId,
            [FromRoute] Guid auctionId,
            ISender sender,
            CancellationToken ct) => TypedResults.Ok());
    }
}