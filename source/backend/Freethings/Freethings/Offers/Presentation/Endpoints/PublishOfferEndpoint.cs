using Freethings.Offers.Application.Commands;
using Freethings.Shared.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Offers.Presentation.Endpoints;

public static class PublishOfferEndpoint
{
    public static void MapPublishOfferEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("{offerId:guid}/publish", async Task<Results<NoContent, NotFound>>(
            [FromRoute] Guid userId,
            [FromRoute] Guid offerId,
            ISender sender,
            CancellationToken ct) =>
        {
            Result result = await sender.Send(new PublishOfferCommand(
                userId,
                offerId
                ), ct);
            
            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.NotFound();
        });
    }
}