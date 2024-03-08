using Freethings.Offers.Infrastructure.Queries.GetOffers;
using Freethings.Offers.Infrastructure.Queries.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Offers.Presentation.GetOffers;

public static class Endpoint
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