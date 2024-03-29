using FluentAssertions;
using FluentAssertions.Execution;
using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Exceptions;
using Freethings.Contracts.Events;
using Freethings.Shared.Abstractions.Domain.Exceptions;

namespace Freethings.UnitTests.Auctions.Domain;

public sealed class AuctionWithManualSelectionTests
{
    [Fact]
    public void CanClaimAuctionItem()
    {
        // arrange
        AuctionAggregate manualAuctionAggregate = AuctionFixtures.CreateAuction(AuctionType.Manual, 10);
        Guid userId = Guid.NewGuid();
        Quantity claimedQuantity = Quantity.Create(3);

        // act
        manualAuctionAggregate.Claim(new AuctionAggregate.ClaimCommand(userId, claimedQuantity), DateTimeOffset.UtcNow);

        // assert
        using (new AssertionScope())
        {
            AuctionEvent.ItemsClaimed commandResult = (AuctionEvent.ItemsClaimed) manualAuctionAggregate.DomainEvents.First();
            
            commandResult.ClaimedById.Should().Be(userId);
            commandResult.ClaimedQuantity.Should().Be(claimedQuantity.Value);
        }
    }
    
    [Fact]
    public void CannotClaimOnSameAuctionTwiceByOneClaimer()
    {
        // arrange
        AuctionAggregate manualAuctionAggregate = AuctionFixtures.CreateAuction(AuctionType.Manual, 10);
        Guid userId = Guid.NewGuid();
        Quantity claimedQuantity = Quantity.Create(3);

        // act
        manualAuctionAggregate.Claim(new AuctionAggregate.ClaimCommand(userId, claimedQuantity), DateTimeOffset.UtcNow);
        Action act = () => manualAuctionAggregate.Claim(new AuctionAggregate.ClaimCommand(userId, claimedQuantity), DateTimeOffset.UtcNow);
        
        // assert
        act.Should().Throw<DomainException>()
            .WithMessage(AuctionExceptions.SameUserCannotCreateTwoClaimsOnOneAuction.Message);
    }

    [Fact]
    public void AuctionItemClaimCanBeReservedManually()
    {
        // arrange
        AuctionAggregate manualAuctionAggregate = AuctionFixtures.CreateAuction(AuctionType.Manual, 10);
        Guid userId = Guid.NewGuid();
        Quantity claimedQuantity = Quantity.Create(3);

        // act
        manualAuctionAggregate.Claim(new AuctionAggregate.ClaimCommand(userId, claimedQuantity), DateTimeOffset.UtcNow);
        manualAuctionAggregate.Reserve(new AuctionAggregate.ReserveCommand(userId));

        // assert
        using (new AssertionScope())
        {
            AuctionEvent.ItemsReserved commandResult = (AuctionEvent.ItemsReserved) manualAuctionAggregate.DomainEvents.Last();
            
            commandResult.Should().NotBeNull();
            commandResult.ReservedById.Should().Be(userId);
        }
    }
    
    [Fact]
    public void CannotReserveIfThereIsNoCorrespondingClaimExists()
    {
        // arrange
        AuctionAggregate manualAuctionAggregate = AuctionFixtures.CreateAuction(AuctionType.Manual, 10);
        Guid userId = Guid.NewGuid();

        // act
        Action act = () => manualAuctionAggregate.Reserve(new AuctionAggregate.ReserveCommand(userId));
        
        // assert
        act.Should().Throw<DomainException>()
            .WithMessage(AuctionExceptions.CannotReserveIfThereIsNoClaimReferenced.Message);
    }
    
    [Fact]
    public void ReserveShouldFailIfWantToReserveMoreThanAvailable()
    {
        // arrange
        AuctionAggregate manualAuctionAggregate = AuctionFixtures
            .CreateAuction(AuctionType.Manual, 10);
        Guid userId = Guid.NewGuid();
        Guid otherUserId = Guid.NewGuid();

        // act
        manualAuctionAggregate.Claim(new AuctionAggregate.ClaimCommand(userId, Quantity.Create(5)), DateTimeOffset.UtcNow);
        manualAuctionAggregate.Reserve(new AuctionAggregate.ReserveCommand(userId));
        manualAuctionAggregate.HandOver(new AuctionAggregate.HandOverCommand(userId));
        
        manualAuctionAggregate.Claim(new AuctionAggregate.ClaimCommand(otherUserId, Quantity.Create(6)), DateTimeOffset.UtcNow);
        Action action = () => manualAuctionAggregate.Reserve(new AuctionAggregate.ReserveCommand(otherUserId));
        
        // assert
        action.Should().Throw<DomainException>()
            .WithMessage(AuctionExceptions.AvailableQuantitySmallerThanAvailable.Message);
    }

    [Fact]
    public void CanHandOverClaimedItems()
    {
        // arrange
        int initialQuantity = 10;
        AuctionAggregate manualAuctionAggregate = AuctionFixtures.CreateAuction(AuctionType.Manual, initialQuantity);
        Guid userId = Guid.NewGuid();
        Quantity claimedQuantity = Quantity.Create(3);

        // act
        manualAuctionAggregate.Claim(new AuctionAggregate.ClaimCommand(userId, claimedQuantity), DateTimeOffset.UtcNow);
        manualAuctionAggregate.Reserve(new AuctionAggregate.ReserveCommand(userId));
        AuctionEvent.ItemsHandedOver? commandResult = manualAuctionAggregate.HandOver(new AuctionAggregate.HandOverCommand(userId));

        // assert
        using (new AssertionScope())
        {
            commandResult.Should().NotBeNull();
            commandResult.HandedOverQuantity.Should().Be(claimedQuantity.Value);
            commandResult.AvailableQuantity.Should().Be(initialQuantity - claimedQuantity.Value);
        }
    }
}