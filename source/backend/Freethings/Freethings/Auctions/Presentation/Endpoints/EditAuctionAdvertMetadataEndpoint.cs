using System.ComponentModel.DataAnnotations;
using Freethings.Auctions.Application.Commands;
using Freethings.Shared.Abstractions.Auth.Context;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class EditAuctionAdvertMetadataEndpoint
{
    private sealed record EditAuctionAdvertMetadataRequestBody(
        string Title,
        string Description) : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
    
    public static RouteGroupBuilder MapEditAuctionAdvertMetadataEndpoint(this RouteGroupBuilder group)
    {
        group.MapPatch("/{auctionId:guid}", async Task<Results<NoContent, NotFound>> (
            [FromRoute] Guid auctionId,
            [FromBody] EditAuctionAdvertMetadataRequestBody request,
            ISender sender,
            ICurrentUser currentUser,
            CancellationToken ct) =>
        {
            BusinessResult businessResult = await sender.Send(new EditAuctionAdvertMetadataCommand(
                currentUser.Identity.Value,
                auctionId,
                request.Title,
                request.Description
            ), ct);

            return businessResult.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.NotFound();
        });

        return group;
    }
}