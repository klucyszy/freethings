using FluentAssertions;
using FluentAssertions.Execution;
using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Exceptions;
using Freethings.Contracts.Events;
using Freethings.Shared.Abstractions.Domain.Exceptions;

namespace Freethings.UnitTests.Auctions;

public sealed class AuctionWithManualSelectionTests
{
    [Fact]
    public void CanClaimAuctionItem()
    {
        // arrange
        Auction manualAuction = AuctionFixtures.CreateAuction(Auction.AuctionType.Manual, 10);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        manualAuction.Claim(new Auction.ClaimCommand(userId, claimedQuantity));

        // assert
        using (new AssertionScope())
        {
            AuctionEvent.ItemsClaimed commandResult = (AuctionEvent.ItemsClaimed) manualAuction.DomainEvents.First();
            
            commandResult.ClaimedById.Should().Be(userId);
            commandResult.ClaimedQuantity.Should().Be(claimedQuantity);
        }
    }
    
    [Fact]
    public void CannotClaimOnSameAuctionTwiceByOneClaimer()
    {
        // arrange
        Auction manualAuction = AuctionFixtures.CreateAuction(Auction.AuctionType.Manual, 10);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        manualAuction.Claim(new Auction.ClaimCommand(userId, claimedQuantity));
        Action act = () => manualAuction.Claim(new Auction.ClaimCommand(userId, claimedQuantity));
        
        // assert
        act.Should().Throw<DomainException>()
            .WithMessage(AuctionExceptions.SameUserCannotCreateTwoClaimsOnOneAuction.Message);
    }

    [Fact]
    public void AuctionItemClaimCanBeReservedManually()
    {
        // arrange
        Auction manualAuction = AuctionFixtures.CreateAuction(Auction.AuctionType.Manual, 10);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        manualAuction.Claim(new Auction.ClaimCommand(userId, claimedQuantity));
        manualAuction.Reserve(new Auction.ReserveCommand(userId));

        // assert
        using (new AssertionScope())
        {
            AuctionEvent.ItemsReserved commandResult = (AuctionEvent.ItemsReserved) manualAuction.DomainEvents.Last();
            
            commandResult.Should().NotBeNull();
            commandResult.ReservedById.Should().Be(userId);
        }
    }
    
    [Fact]
    public void CannotReserveIfThereIsNoCorrespondingClaimExists()
    {
        // arrange
        Auction manualAuction = AuctionFixtures.CreateAuction(Auction.AuctionType.Manual, 10);
        Guid userId = Guid.NewGuid();

        // act
        Action act = () => manualAuction.Reserve(new Auction.ReserveCommand(userId));
        
        // assert
        act.Should().Throw<DomainException>()
            .WithMessage(AuctionExceptions.CannotReserveIfThereIsNoClaimReferenced.Message);
    }
    
    [Fact]
    public void ReserveShouldFailIfWantToReserveMoreThanAvailable()
    {
        // arrange
        Auction manualAuction = AuctionFixtures
            .CreateAuction(Auction.AuctionType.Manual, 10);
        Guid userId = Guid.NewGuid();
        Guid otherUserId = Guid.NewGuid();

        // act
        manualAuction.Claim(new Auction.ClaimCommand(userId, 5));
        manualAuction.Reserve(new Auction.ReserveCommand(userId));
        manualAuction.HandOver(new Auction.HandOverCommand(userId));
        
        manualAuction.Claim(new Auction.ClaimCommand(otherUserId, 6));
        Action action = () => manualAuction.Reserve(new Auction.ReserveCommand(otherUserId));
        
        // assert
        action.Should().Throw<DomainException>()
            .WithMessage(AuctionExceptions.AvailableQuantitySmallerThanAvailable.Message);
    }

    [Fact]
    public void CanHandOverClaimedItems()
    {
        // arrange
        int initialQuantity = 10;
        Auction manualAuction = AuctionFixtures.CreateAuction(Auction.AuctionType.Manual, initialQuantity);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        manualAuction.Claim(new Auction.ClaimCommand(userId, claimedQuantity));
        manualAuction.Reserve(new Auction.ReserveCommand(userId));
        AuctionEvent.ItemsHandedOver? commandResult = manualAuction.HandOver(new Auction.HandOverCommand(userId));

        // assert
        using (new AssertionScope())
        {
            commandResult.Should().NotBeNull();
            commandResult.HandedOverQuantity.Should().Be(claimedQuantity);
            commandResult.AvailableQuantity.Should().Be(initialQuantity - claimedQuantity);
        }
    }
}