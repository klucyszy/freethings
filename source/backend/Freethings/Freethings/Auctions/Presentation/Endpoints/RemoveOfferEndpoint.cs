using Freethings.Auctions.Application.Commands;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class RemoveAuctionAdvertEndpoint
{
    public static void MapRemoveAuctionAdvertEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{auctionId:guid}", async Task<Results<NoContent, NotFound>>(
            [FromRoute] Guid userId,
            [FromRoute] Guid auctionId,
            ISender sender,
            CancellationToken ct) =>
        {
            BusinessResult businessResult = await sender.Send(new RemoveAuctionAdvertCommand(
                userId,
                auctionId
                ), ct);
            
            return businessResult.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.NotFound();
        });
    }
}