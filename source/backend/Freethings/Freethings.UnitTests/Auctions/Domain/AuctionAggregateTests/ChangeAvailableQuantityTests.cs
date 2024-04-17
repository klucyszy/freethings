using FluentAssertions;
using FluentAssertions.Execution;
using Freethings.Auctions.Domain;

namespace Freethings.UnitTests.Auctions.Domain.AuctionAggregateTests;

public sealed class ChangeAvailableQuantityTests
{
    [Fact]
    public void WhenQuantityChangedToLessToReservationsIsCancelled()
    {
        // arrange
        int initialQuantity = 10;
        AuctionAggregate auctionAggregate = AuctionFixtures.CreateAuction(AuctionType.Manual, initialQuantity);
        AuctionAggregate.ClaimCommand claim1 = new(Guid.NewGuid(), Quantity.Create(3));
        AuctionAggregate.ClaimCommand claim2 = new(Guid.NewGuid(), Quantity.Create(1));
        
        // act
        auctionAggregate.Claim(claim1, DateTimeOffset.UtcNow);
        auctionAggregate.Reserve(new AuctionAggregate.ReserveCommand(claim1.ClaimedById));
        auctionAggregate.Claim(claim2, DateTimeOffset.UtcNow);
        auctionAggregate.Reserve(new AuctionAggregate.ReserveCommand(claim2.ClaimedById));
        
        auctionAggregate.ChangeAvailableQuantity(Quantity.Create(1));
        
        // assert
        using (new AssertionScope())
        {
            auctionAggregate.AvailableQuantity.Value.Should().Be(1);
            auctionAggregate.Claims.Count.Should().Be(2);
            auctionAggregate.Claims
                .FirstOrDefault(x => x.ClaimedById == claim1.ClaimedById)!
                .IsReserved.Should().BeFalse();
            auctionAggregate.Claims
                .FirstOrDefault(x => x.ClaimedById == claim2.ClaimedById)!
                .IsReserved.Should().BeTrue();
        }
    }   
}