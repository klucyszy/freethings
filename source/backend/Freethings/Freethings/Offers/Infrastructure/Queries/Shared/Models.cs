namespace Freethings.Offers.Infrastructure.Queries.Shared;

public sealed record OfferDto(
    Guid Id,
    string Title,
    string Description,
    string State);