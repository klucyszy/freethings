using System.ComponentModel.DataAnnotations;
using Freethings.Auctions.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class ReserveAuctionItemEndpoint
{
    private sealed record ReserveAuctionItemQueryParameters(
        int Quantity = 1) : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Quantity < 1)
            {
                yield return new ValidationResult("Quantity must be greater than 0");
            }
        }
    }

    public static RouteGroupBuilder MapReserveAuctionItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/{auctionId:guid}/claim/{claimId}/reserve", async (
                [FromRoute] Guid auctionId,
                [FromRoute] Guid claimId,
                [AsParameters] ReserveAuctionItemQueryParameters parameters,
                ISender sender,
                CancellationToken ct)
            =>
        {
            await sender.Send(new ReserveClaimedItemsCommand(
                auctionId,
                claimId,
                parameters.Quantity,
                true), ct);

            return TypedResults.NoContent();
        });

        return group;
    }
}