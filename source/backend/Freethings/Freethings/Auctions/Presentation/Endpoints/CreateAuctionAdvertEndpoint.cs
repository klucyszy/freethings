using System.ComponentModel.DataAnnotations;
using Freethings.Auctions.Application.Commands;
using Freethings.Auctions.Domain;
using Freethings.Shared.Abstractions.Auth.Context;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Infrastructure.Api.Results;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class CreateAuctionAdvertEndpoint
{
    private sealed record CreateAuctionAdvertRequestBody(
        AuctionType Type,
        string Title,
        string Description,
        int Quantity) : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                yield return new ValidationResult("Title is required");
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                yield return new ValidationResult("Description is required");
            }

            if (Quantity < 1)
            {
                yield return new ValidationResult("Quantity must be greater than 0");
            }
        }
    }

    public static RouteGroupBuilder MapCreateAuctionAdvertEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("", async (
            [FromBody] CreateAuctionAdvertRequestBody request,
            ISender sender,
            ICurrentUser currentUser,
            CancellationToken ct,
            [FromQuery] Guid? userId = null) =>
        {
            BusinessResult<Guid> result = await sender.Send(new CreateAuctionAdvertCommand(
                currentUser.Identity.Value,
                request.Type,
                request.Title,
                request.Description,
                request.Quantity
            ), ct);

            return ApiResultMapper.MapToEndpointResult(result);
        });

        return group;
    }
}