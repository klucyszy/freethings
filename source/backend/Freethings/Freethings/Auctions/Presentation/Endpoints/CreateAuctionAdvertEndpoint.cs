using Freethings.Auctions.Application.Commands;
using Freethings.Auctions.Domain;
using Freethings.Shared.Abstractions.Auth.Context;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class CreateAuctionAdvertEndpoint
{
    private sealed record CreateAuctionAdvertRequest
    {
        public AuctionType Type { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public int Quantity { get; init; }
    }

    public static RouteGroupBuilder MapCreateAuctionAdvertEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("", async (
                [FromBody] CreateAuctionAdvertRequest request,
                ISender sender,
                ICurrentUser currentUser,
                CancellationToken ct,
                [FromQuery] Guid? userId = null) =>
            {
                if (userId is not null && !currentUser.IsAdmin)
                {
                    return TypedResults.Forbid();
                }

                userId ??= currentUser.Identity;

                BusinessResult<Guid> result = await sender.Send(new CreateAuctionAdvertCommand(
                    userId!.Value,
                    request.Type,
                    request.Title,
                    request.Description,
                    request.Quantity
                ), ct);

                return ApiResultMapper.MapToEndpointResult(result);
            })
            .RequireAuthorization();

        return group;
    }
}