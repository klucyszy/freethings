using Freethings.Offers.Application.Commands;
using Freethings.Shared;
using Freethings.Shared.Infrastructure;
using MediatR;
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
            Result result = await sender.Send(new EditOfferCommand(
                userId,
                offerId,
                request.Title,
                request.Description
                ), ct);
            
            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.NotFound();
        });
    }
}