using Freethings.Offers.Application.Commands.AddOffer;
using MediatR;

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
            AddOfferRequest request,
            ISender sender,
            CancellationToken ct) =>
        {
            Guid result = await sender.Send(new AddOfferCommand(
                request.Title,
                request.Description
                ), ct);

            return TypedResults.Ok(result);
        });
    }
}