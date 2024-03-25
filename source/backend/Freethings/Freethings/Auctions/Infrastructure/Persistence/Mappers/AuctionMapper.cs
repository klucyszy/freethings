using Freethings.Auctions.Domain;
using Freethings.Auctions.Infrastructure.Persistence.Entities;
using Freethings.Shared.Infrastructure.Persistence;
using Freethings.Shared.Infrastructure.Time;

namespace Freethings.Auctions.Infrastructure.Persistence.Mappers;

internal static class AuctionMapper
{
    public static AuctionEntity ToEntity(this Auction auction, AuctionEntity currentEntityState)
    {
        currentEntityState.Quantity = auction.AvailableQuantity;
        currentEntityState.Claims.Update(
            auction.Claims.ToList(),
            (entity, aggregate) => entity.UserId == aggregate.ClaimedById,
            auctionClaim => new AuctionClaimEntity
            {
                UserId = auctionClaim.ClaimedById,
                Quantity = auctionClaim.Quantity,
                Comment = auctionClaim.Comment,
                Timestamp = auctionClaim.Timestamp,
                IsReserved = auctionClaim.IsReserved
            });
        
        return currentEntityState;
    }
    
    public static Auction ToAggregate(this AuctionEntity auctionEntity, ICurrentTime currentTime)
    {
        return new Auction(
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