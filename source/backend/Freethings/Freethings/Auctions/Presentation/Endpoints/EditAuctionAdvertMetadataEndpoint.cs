using Freethings.Auctions.Application.Commands;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class EditAuctionAdvertMetadataEndpoint
{
    private sealed record EditAuctionAdvertMetadataRequest
    {
        public string Title { get; init; }
        public string Description { get; init; }
    }
    
    public static RouteGroupBuilder MapEditAuctionAdvertMetadataEndpoint(this RouteGroupBuilder group)
    {
        group.MapPatch("/{auctionId:guid}", async Task<Results<NoContent, NotFound>>(
            [FromRoute] Guid userId,
            [FromRoute] Guid auctionId,
            [FromBody] EditAuctionAdvertMetadataRequest request,
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
        })
        .RequireAuthorization();

        return group;
    }
}