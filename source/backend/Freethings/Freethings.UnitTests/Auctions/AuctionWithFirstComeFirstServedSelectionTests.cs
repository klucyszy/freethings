using FluentAssertions;
using FluentAssertions.Execution;
using Freethings.Auctions.Domain;
using Freethings.Contracts.Events;

namespace Freethings.UnitTests.Auctions;

public class AuctionWithFirstComeFirstServedSelectionTests
{
    [Fact]
    public void UserClaimBecomesAutomaticallyReservedIfAvailable()
    {
        // arrange
        int initialQuantity = 10;
        Auction auction = AuctionFixtures.CreateAuction(Auction.AuctionType.FirstComeFirstServed, initialQuantity);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        auction.Claim(new Auction.ClaimCommand(userId, claimedQuantity));

        // assert
        using (new AssertionScope())
        {
            AuctionEvent.ItemsReserved commandResult = (AuctionEvent.ItemsReserved) auction.DomainEvents.First();
            
            commandResult.ClaimedById.Should().Be(userId);
            commandResult.ClaimedQuantity.Should().Be(claimedQuantity);
            commandResult.AvailableQuantity.Should().Be(initialQuantity - claimedQuantity);
        }
    }
}