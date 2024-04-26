using System.ComponentModel.DataAnnotations;
using Freethings.Auctions.Application.Commands;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class ClaimAuctionItemEndpoint
{
    private sealed record QueryParameters(
        [Range(1, Int32.MaxValue)] int Quantity = 1);

    public static RouteGroupBuilder MapClaimAuctionItemEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost("/{auctionId:guid}/claim", async (
                    [FromQuery] Guid userId,
                    [FromRoute] Guid auctionId,
                    [AsParameters] QueryParameters parameters,
                    ISender sender,
                    CancellationToken ct)
                =>
            {
                BusinessResult result = await sender.Send(new ClaimItemsCommand(
                    auctionId,
                    userId,
                    parameters.Quantity), ct);

                return ApiResultMapper.MapToEndpointResult(result);
            })
            .RequireAuthorization();

        return group;
    }
}