using Freethings.Offers.Application.Commands;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
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
            BusinessResult businessResult = await sender.Send(new RemoveOfferCommand(
                userId,
                offerId
                ), ct);
            
            return businessResult.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.NotFound();
        });
    }
}