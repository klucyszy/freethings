using Freethings.Offers.Infrastructure.Queries.GetOffer;
using Freethings.Offers.Infrastructure.Queries.GetOffers;
using Freethings.Offers.Infrastructure.Queries.Shared;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Freethings.Offers.Presentation.GetOffer;

public static class Endpoint
{
    public static void MapGetOfferEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{offerId:guid}", async Task<Results<Ok<OfferDto>, NotFound>> (
            [FromRoute] Guid userId,
            [FromRoute] Guid offerId,
            ISender sender,
            CancellationToken ct) =>
        {
            OfferDto result = await sender.Send(new GetOfferQuery(
                userId,
                offerId
            ), ct);

            return result is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(result);
        });
    }
}