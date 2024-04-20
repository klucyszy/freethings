namespace Freethings.Auctions.Infrastructure.Queries.Models;

public sealed record AuctionDto(
    Guid Id,
    string Title,
    string Description,
    string State,
    int AvailableItems,
    int ClaimedItems);