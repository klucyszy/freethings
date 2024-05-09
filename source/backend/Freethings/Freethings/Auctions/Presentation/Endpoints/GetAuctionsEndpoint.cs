using System.ComponentModel.DataAnnotations;
using Freethings.Auctions.Infrastructure.Queries;
using Freethings.Auctions.Infrastructure.Queries.Models;
using Freethings.Shared.Abstractions.Auth.Context;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class GetAuctionsEndpoint
{
    private sealed record GetAuctionsEndpointQueryParameters(
        string SearchText,
        int Page = 1,
        int ElementsPerPage = 10) : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SearchText?.Length is < 3 or > 25)
            {
                yield return new ValidationResult($"{nameof(SearchText)} must be in range 3-25 characters");
            }

            if (Page < 1)
            {
                yield return new ValidationResult("Page must be greater than 0");
            }

            if (ElementsPerPage is < 10 or > 100)
            {
                yield return new ValidationResult($"{nameof(ElementsPerPage)} must be in range 10-100");
            }
        }
    };

    public static RouteGroupBuilder MapGetAuctionsEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("", async (
            [AsParameters] GetAuctionsEndpointQueryParameters parameters,
            ISender sender,
            ICurrentUser currentUser,
            CancellationToken ct) =>
        {
            List<AuctionDto> result = await sender.Send(new GetAuctionsQuery(
                currentUser.Identity.Value,
                parameters.Page,
                parameters.ElementsPerPage,
                parameters.SearchText
            ), ct);

            return TypedResults.Ok(result);
        });
        
        return group;
    }
}