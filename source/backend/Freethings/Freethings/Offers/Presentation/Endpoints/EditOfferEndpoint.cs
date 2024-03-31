using Freethings.Offers.Application.Commands;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Offers.Presentation.Endpoints;

public static class EditOfferEndpoint
{
    private sealed record EditOfferRequest
    {
        public string Title { get; init; }
        public string Description { get; init; }
    }
    
    public static void MapEditOfferEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/{offerId:guid}", async Task<Results<NoContent, NotFound>>(
            [FromRoute] Guid userId,
            [FromRoute] Guid offerId,
            [FromBody] EditOfferRequest request,
            ISender sender,
            CancellationToken ct) =>
        {
            BusinessResult businessResult = await sender.Send(new EditOfferCommand(
                userId,
                offerId,
                request.Title,
                request.Description
                ), ct);
            
            return businessResult.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.NotFound();
        });
    }
}