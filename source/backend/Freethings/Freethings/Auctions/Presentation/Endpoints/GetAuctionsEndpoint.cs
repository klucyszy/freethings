using Freethings.Auctions.Infrastructure.Queries;
using Freethings.Auctions.Infrastructure.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class GetAuctionsEndpoint
{
    public static RouteGroupBuilder MapGetAuctionsEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("", async (
            [FromQuery] Guid userId,
            ISender sender,
            CancellationToken ct) =>
        {
            List<AuctionDto> result = await sender.Send(new GetAuctionsQuery(
                userId
                ), ct);

            return TypedResults.Ok(result);
        })
        .RequireAuthorization();

        return group;
    }
}