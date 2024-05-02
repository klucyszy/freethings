using System.ComponentModel.DataAnnotations;
using Freethings.Auctions.Application.Commands;
using Freethings.Shared.Abstractions.Auth.Context;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Infrastructure.Api.Results;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class ChangeAvailableQuantityEndpoint
{
    private sealed record ChangeAvailableQuantityRequestBody(
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

    public static RouteGroupBuilder MapChangeAvailableQuantityEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPut("/{auctionId:guid}/quantity", async (
                    [FromRoute] Guid auctionId,
                    [FromBody] ChangeAvailableQuantityRequestBody parameters,
                    ISender sender,
                    ICurrentUser currentUser,
                    CancellationToken ct)
                =>
            {
                BusinessResult result = await sender.Send(new ChangeAvailableItemsQuantityCommand(
                    auctionId,
                    currentUser.Identity.Value,
                    parameters.Quantity), ct);

                return ApiResultMapper.MapToEndpointResult(result);
            });

        return group;
    }
}