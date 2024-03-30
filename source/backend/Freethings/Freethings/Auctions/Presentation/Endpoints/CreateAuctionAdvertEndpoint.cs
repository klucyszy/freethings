using Freethings.Auctions.Application.Commands;
using Freethings.Auctions.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class CreateAuctionAdvertEndpoint
{
    private sealed record RequestBody
    {
        public AuctionType Type { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public int Quantity { get; init; }
    }
    
    public static RouteGroupBuilder MapCreateAuctionAdvertEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("", async (
            [FromRoute] Guid userId,
            [FromBody] RequestBody request,
            ISender sender,
            CancellationToken ct) =>
        {
            Guid result = await sender.Send(new CreateAuctionAdvertCommand(
                userId,
                request.Type,
                request.Title,
                request.Description,
                request.Quantity
                ), ct);

            return TypedResults.Ok(result);
        });
        
        return group;
    }
}