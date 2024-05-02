using System.ComponentModel.DataAnnotations;
using Freethings.Auctions.Application.Commands;
using Freethings.Shared.Abstractions.Auth.Context;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Infrastructure.Api.Results;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class ClaimAuctionItemEndpoint
{
    private sealed record ClaimAuctionItemQueryParameters(
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

    public static RouteGroupBuilder MapClaimAuctionItemEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost("/{auctionId:guid}/claim", async (
                    [FromRoute] Guid auctionId,
                    [AsParameters] ClaimAuctionItemQueryParameters parameters,
                    ISender sender,
                    ICurrentUser currentUser,
                    CancellationToken ct)
                =>
            {
                BusinessResult result = await sender.Send(new ClaimItemsCommand(
                    auctionId,
                    currentUser.Identity.Value,
                    parameters.Quantity), ct);

                return ApiResultMapper.MapToEndpointResult(result);
            });

        return group;
    }
}