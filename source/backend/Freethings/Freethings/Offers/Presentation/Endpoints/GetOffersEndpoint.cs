using Freethings.Offers.Infrastructure.Queries;
using Freethings.Offers.Infrastructure.Queries.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Offers.Presentation.Endpoints;

public static class GetOffersEndpoint
{
    public static void MapGetOffersEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("", async (
            [FromRoute] Guid userId,
            ISender sender,
            CancellationToken ct) =>
        {
            List<OfferDto> result = await sender.Send(new GetOffersQuery(
                userId
                ), ct);

            return TypedResults.Ok(result);
        });
    }
}