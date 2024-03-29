using System.ComponentModel.DataAnnotations;
using Freethings.Auctions.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class ReserveAuctionItemEndpoint
{
    private sealed record QueryParameters(
        [Range(1, Int32.MaxValue)] int Quantity = 1);

    public static RouteGroupBuilder MapReserveAuctionItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("claim/{claimId}/reserve", async (
                [FromRoute] Guid userId,
                [FromRoute] Guid auctionId,
                [FromRoute] Guid claimId,
                [AsParameters] QueryParameters parameters,
                ISender sender,
                CancellationToken ct)
            =>
        {
            await sender.Send(new ReserveClaimedItemsCommand(
                auctionId,
                claimId,
                parameters.Quantity,
                true), ct);

            return TypedResults.NoContent();
        });

        return group;
    }
}