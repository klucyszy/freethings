using FluentAssertions;
using FluentAssertions.Execution;
using Freethings.Auctions.Domain;
using Freethings.Contracts.Events;

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
        
        auctionAggregate.ChangeAvailableQuantity(Quantity.Create(1), DateTimeOffset.UtcNow);
        
        // assert
        using (new AssertionScope())
        {
            auctionAggregate.AvailableQuantity.Value.Should().Be(1);
            auctionAggregate.DomainEvents.OfType<AuctionEvent.ItemsReservationCancelled>()
                .Count().Should().Be(1);
            auctionAggregate.Claims.Count.Should().Be(2);
            auctionAggregate.Claims
                .FirstOrDefault(x => x.ClaimedById == claim1.ClaimedById)!
                .IsReserved.Should().BeFalse();
            auctionAggregate.Claims
                .FirstOrDefault(x => x.ClaimedById == claim2.ClaimedById)!
                .IsReserved.Should().BeTrue();
        }
    }
    
    [Fact]
    public void WhenQuantityChangedToLessToReservationsIsCancelled2()
    {
        // arrange
        int initialQuantity = 10;
        AuctionAggregate auctionAggregate = AuctionFixtures.CreateAuction(AuctionType.Manual, initialQuantity);
        AuctionAggregate.ClaimCommand claim1 = new(Guid.NewGuid(), Quantity.Create(1));
        AuctionAggregate.ClaimCommand claim2 = new(Guid.NewGuid(), Quantity.Create(1));
        AuctionAggregate.ClaimCommand claim3 = new(Guid.NewGuid(), Quantity.Create(1));
        AuctionAggregate.ClaimCommand claim4 = new(Guid.NewGuid(), Quantity.Create(1));
        
        // act
        auctionAggregate.Claim(claim1, DateTimeOffset.UtcNow);
        auctionAggregate.Reserve(new AuctionAggregate.ReserveCommand(claim1.ClaimedById));
        auctionAggregate.Claim(claim2, DateTimeOffset.UtcNow);
        auctionAggregate.Reserve(new AuctionAggregate.ReserveCommand(claim2.ClaimedById));
        auctionAggregate.Claim(claim3, DateTimeOffset.UtcNow);
        auctionAggregate.Reserve(new AuctionAggregate.ReserveCommand(claim3.ClaimedById));
        auctionAggregate.Claim(claim4, DateTimeOffset.UtcNow);
        auctionAggregate.Reserve(new AuctionAggregate.ReserveCommand(claim4.ClaimedById));
        
        auctionAggregate.ChangeAvailableQuantity(Quantity.Create(2), DateTimeOffset.UtcNow);
        
        // assert
        using (new AssertionScope())
        {
            auctionAggregate.AvailableQuantity.Value.Should().Be(2);
            auctionAggregate.DomainEvents.OfType<AuctionEvent.ItemsReservationCancelled>()
                .Count().Should().Be(2);
            auctionAggregate.Claims
                .FirstOrDefault(x => x.ClaimedById == claim1.ClaimedById)!
                .IsReserved.Should().BeTrue();
            auctionAggregate.Claims
                .FirstOrDefault(x => x.ClaimedById == claim2.ClaimedById)!
                .IsReserved.Should().BeTrue();
            auctionAggregate.Claims
                .FirstOrDefault(x => x.ClaimedById == claim3.ClaimedById)!
                .IsReserved.Should().BeFalse();
            auctionAggregate.Claims
                .FirstOrDefault(x => x.ClaimedById == claim4.ClaimedById)!
                .IsReserved.Should().BeFalse();
        }
    }
}