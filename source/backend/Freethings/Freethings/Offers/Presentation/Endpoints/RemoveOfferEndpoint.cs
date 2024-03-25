using Freethings.Offers.Application.Commands;
using Freethings.Shared.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Offers.Presentation.Endpoints;

public static class RemoveOfferEndpoint
{
    public static void MapRemoveOfferEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{offerId:guid}", async Task<Results<NoContent, NotFound>>(
            [FromRoute] Guid userId,
            [FromRoute] Guid offerId,
            ISender sender,
            CancellationToken ct) =>
        {
            Result result = await sender.Send(new RemoveOfferCommand(
                userId,
                offerId
                ), ct);
            
            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.NotFound();
        });
    }
}