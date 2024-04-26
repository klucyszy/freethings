using System.ComponentModel.DataAnnotations;
using Freethings.Auctions.Application.Commands;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class ChangeAvailableQuantityEndpoint
{
    private sealed record ChangeAvailableQuantityRequestBody(
        [Range(1, Int32.MaxValue)] int Quantity = 1);

    public static RouteGroupBuilder MapChangeAvailableQuantityEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPut("/{auctionId:guid}/quantity", async (
                    [FromQuery] Guid userId,
                    [FromRoute] Guid auctionId,
                    [FromBody] ChangeAvailableQuantityRequestBody parameters,
                    ISender sender,
                    CancellationToken ct)
                =>
            {
                BusinessResult result = await sender.Send(new ChangeAvailableItemsQuantityCommand(
                    auctionId,
                    userId,
                    parameters.Quantity), ct);

                return ApiResultMapper.MapToEndpointResult(result);
            })
            .RequireAuthorization();

        return group;
    }
}