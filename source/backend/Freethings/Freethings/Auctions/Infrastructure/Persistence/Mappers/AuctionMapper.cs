using Freethings.Auctions.Domain;

namespace Freethings.Auctions.Infrastructure.Persistence.Mappers;

internal static class AuctionMapper
{
    public static AuctionAggregate ToAggregate(this AuctionAdvert auctionEntity, ICurrentTime currentTime)
    {
        return new AuctionAggregate(
            auctionEntity.Id,
            auctionEntity.Claims.ToList(),
            auctionEntity.Quantity,
            auctionEntity.Type);
    }
}