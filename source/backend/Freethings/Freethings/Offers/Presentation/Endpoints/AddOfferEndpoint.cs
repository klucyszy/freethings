using Freethings.Offers.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Offers.Presentation.Endpoints;

public static class AddOfferEndpoint
{
    private sealed record AddOfferRequest
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public int Quantity { get; init; }
    }
    
    public static void MapAddOfferEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("", async (
            [FromRoute] Guid userId,
            [FromBody] AddOfferRequest request,
            ISender sender,
            CancellationToken ct) =>
        {
            Guid result = await sender.Send(new AddOfferCommand(
                userId,
                request.Title,
                request.Description,
                request.Quantity
                ), ct);

            return TypedResults.Ok(result);
        });
    }
}