using Freethings.Auctions.Domain;

namespace Freethings.UnitTests.Auctions;

public static class AuctionFixtures
{
    public static AuctionAggregate CreateAuction(AuctionType auctionType, int availableQuantity)
    {
        AuctionAggregate auctionAggregate = new AuctionAggregate(
            Guid.NewGuid(),
            new List<AuctionClaim>(),
            Quantity.Create(availableQuantity),
            auctionType);

        return auctionAggregate;
    }
}