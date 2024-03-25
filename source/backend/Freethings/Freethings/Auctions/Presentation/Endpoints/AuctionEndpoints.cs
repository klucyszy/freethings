using System.ComponentModel.DataAnnotations;
using Freethings.Auctions.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class ClaimAuctionItemEndpoint
{
    private sealed record ClaimItemsQueryParameters(
        [Range(1, Int32.MaxValue)] int Quantity = 1);

    public static RouteGroupBuilder MapAuctionEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("claim", async (
                [FromRoute] Guid userId,
                [FromRoute] Guid auctionId,
                [AsParameters] ClaimItemsQueryParameters parameters,
                ISender sender,
                CancellationToken ct)
            =>
        {
            await sender.Send(new ClaimItemCommand(auctionId, userId, parameters.Quantity), ct);

            return TypedResults.NoContent();
        });

        return group;
    }
}