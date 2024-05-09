using System.ComponentModel.DataAnnotations;
using Freethings.Auctions.Infrastructure.Queries;
using Freethings.Auctions.Infrastructure.Queries.Models;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class SearchAuctionsEndpoint
{
    private sealed record SearchAuctionsEndpointQueryParameters(
        string SearchText,
        string Category,
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

    public static RouteGroupBuilder MapSearchAuctionsEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/search", async (
            [AsParameters] SearchAuctionsEndpointQueryParameters parameters,
            ISender sender,
            CancellationToken ct) =>
        {
            List<AuctionDto> result = await sender.Send(new SearchAuctionsQuery(
                parameters.Page,
                parameters.ElementsPerPage,
                parameters.SearchText,
                parameters.Category
            ), ct);

            return TypedResults.Ok(result);
        })
        .AllowAnonymous();
        
        return group;
    }
}