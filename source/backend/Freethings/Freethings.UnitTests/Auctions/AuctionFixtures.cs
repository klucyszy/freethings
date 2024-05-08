using Freethings.Auctions.Domain;

namespace Freethings.UnitTests.Auctions;

public static class AuctionFixtures
{
    public static AuctionAggregate CreateAuction(AuctionType auctionType, int availableQuantity)
    {
        AuctionAggregate auctionAggregate = new AuctionAggregate(
            Guid.NewGuid(),
            [],
            Quantity.Create(availableQuantity),
            auctionType);

        return auctionAggregate;
    }
}