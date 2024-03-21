using Freethings.Auctions.Domain;

namespace Freethings.UnitTests.Auctions;

public static class AuctionFixtures
{
    public static Auction CreateAuction(Auction.AuctionType auctionType, int availableQuantity)
    {
        Auction auction = new Auction(
            new List<AuctionClaim>(),
            availableQuantity,
            auctionType);

        return auction;
    }
}