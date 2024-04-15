using Freethings.Auctions.Application.Commands;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class EditAuctionEndpoint
{
    private sealed record RequestBody
    {
        public string Title { get; init; }
        public string Description { get; init; }
    }
    
    public static void MapEditAuctionEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/{auctionId:guid}", async Task<Results<NoContent, NotFound>>(
            [FromRoute] Guid userId,
            [FromRoute] Guid auctionId,
            [FromBody] RequestBody request,
            ISender sender,
            CancellationToken ct) =>
        {
            BusinessResult businessResult = await sender.Send(new EditAuctionAdvertMetadataCommand(
                userId,
                auctionId,
                request.Title,
                request.Description
                ), ct);
            
            return businessResult.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.NotFound();
        });
    }
}