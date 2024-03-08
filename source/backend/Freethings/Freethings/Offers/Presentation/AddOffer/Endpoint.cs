using Freethings.Offers.Application.Commands.AddOffer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Offers.Presentation.AddOffer;

public static class Endpoint
{
    private sealed record AddOfferRequest
    {
        public string Title { get; init; }
        public string Description { get; init; }
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
                request.Description
                ), ct);

            return TypedResults.Ok(result);
        });
    }
}