using Freethings.Auctions.Domain;
using Freethings.Auctions.Infrastructure.Persistence.Entities;
using Freethings.Shared.Infrastructure.Persistence;

namespace Freethings.Auctions.Infrastructure.Persistence.Mappers;

internal static class AuctionMapper
{
    public static AuctionEntity ToEntity(this Auction auction, AuctionEntity currentEntityState)
    {
        currentEntityState.Quantity = auction.AvailableQuantity;
        currentEntityState.Claims.Update(
            auction.Claims.ToList(),
            (entity, aggregate) => entity.UserId == aggregate.ClaimedById,
            auctionClaim => new AuctionClaimEntity(
                auctionClaim.ClaimedById,
                auctionClaim.Quantity,
                auctionClaim.Comment,
                auctionClaim.Timestamp,
                auctionClaim.IsReserved)
        );

        return currentEntityState;
    }

    public static Auction ToAggregate(this AuctionEntity auctionEntity, ICurrentTime currentTime)
    {
        return new Auction(
            auctionEntity.Id,
            auctionEntity.Claims.Select(e => new AuctionClaim(
                    e.UserId,
                    e.Quantity,
                    e.Comment,
                    e.Timestamp,
                    e.IsReserved))
                .ToList(),
            auctionEntity.Quantity,
            auctionEntity.Type,
            currentTime);
    }
}