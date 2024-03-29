using Freethings.Auctions.Domain;

namespace Freethings.Auctions.Infrastructure.Persistence.Mappers;

internal static class AuctionMapper
{
    public static AuctionAdvert ToEntity(this AuctionAggregate auctionAggregate, AuctionAdvert currentEntityState)
    {
        // TODO: Do the mapping again
        // currentEntityState.Quantity = auctionAggregate.AvailableQuantity;
        // currentEntityState.Claims.Update(
        //     auctionAggregate.Claims.ToList(),
        //     (entity, aggregate) => entity.UserId == aggregate.ClaimedById,
        //     auctionClaim => new AuctionClaimEntity(
        //         auctionClaim.ClaimedById,
        //         auctionClaim.Quantity,
        //         auctionClaim.Comment,
        //         auctionClaim.Timestamp,
        //         auctionClaim.IsReserved)
        // );

        return currentEntityState;
    }

    public static AuctionAggregate ToAggregate(this AuctionAdvert auctionEntity, ICurrentTime currentTime)
    {
        return new AuctionAggregate(
            auctionEntity.Id,
            auctionEntity.Claims.ToList(),
            auctionEntity.Quantity,
            auctionEntity.Type,
            currentTime);
    }
}