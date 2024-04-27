using Freethings.Auctions.Infrastructure.Queries;
using Freethings.Auctions.Infrastructure.Queries.Models;
using Freethings.Shared.Abstractions.Auth.Context;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Auctions.Presentation.Endpoints;

public static class GetAuctionsEndpoint
{
    private sealed record GetAuctionsEndpointQueryParameters(
        int Page,
        int ElementsPerPage,
        string SearchText);
    
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
        })
        .RequireAuthorization();

        return group;
    }
}