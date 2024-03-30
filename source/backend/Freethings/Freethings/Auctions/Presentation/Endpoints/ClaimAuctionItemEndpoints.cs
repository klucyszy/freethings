using System.ComponentModel.DataAnnotations;
using Freethings.Auctions.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class ClaimAuctionItemEndpoint
{
    private sealed record QueryParameters(
        [Range(1, Int32.MaxValue)] int Quantity = 1);
    
    public static RouteGroupBuilder MapClaimAuctionItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/{auctionId:guid}/claim", async (
                [FromRoute] Guid userId,
                [FromRoute] Guid auctionId,
                [AsParameters] QueryParameters parameters,
                ISender sender,
                CancellationToken ct)
            =>
        {
            await sender.Send(new ClaimItemsCommand(
                auctionId,
                userId,
                parameters.Quantity), ct);

            return TypedResults.NoContent();
        });
        
        return group;
    }
}