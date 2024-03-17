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
    
    [Fact]
    public void IfThereIsLessThanRequestedQuantityAvailableItIsReservedWhatIsLeft()
    {
        // arrange
        int initialQuantity = 10;
        Auction auction = AuctionFixtures.CreateAuction(Auction.AuctionType.FirstComeFirstServed, initialQuantity);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 15;

        // act
        auction.Claim(new Auction.ClaimCommand(userId, claimedQuantity));

        // assert
        using (new AssertionScope())
        {
            AuctionEvent.ItemsReserved commandResult = (AuctionEvent.ItemsReserved) auction.DomainEvents.First();
            
            commandResult.ClaimedById.Should().Be(userId);
            commandResult.ClaimedQuantity.Should().Be(initialQuantity);
            commandResult.AvailableQuantity.Should().Be(0);
        }
    }

    [Fact]
    public void IfAllItemsAlreadyReservedItemsAreOnlyClaimed()
    {
        // arrange
        int initialQuantity = 0;
        Auction auction = AuctionFixtures.CreateAuction(Auction.AuctionType.FirstComeFirstServed, initialQuantity);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 5;

        // act
        auction.Claim(new Auction.ClaimCommand(userId, claimedQuantity));

        // assert
        using (new AssertionScope())
        {
            AuctionEvent.ItemsClaimed commandResult = (AuctionEvent.ItemsClaimed) auction.DomainEvents.First();
            
            commandResult.ClaimedById.Should().Be(userId);
            commandResult.ClaimedQuantity.Should().Be(claimedQuantity);
            commandResult.AvailableQuantity.Should().Be(0);
        }
    }
}