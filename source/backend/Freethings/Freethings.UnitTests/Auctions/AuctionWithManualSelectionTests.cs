using FluentAssertions;
using FluentAssertions.Execution;
using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Exceptions;
using Freethings.Contracts.Events;
using Freethings.Offers.Domain.Entities;
using Freethings.Shared.Exceptions;

namespace Freethings.UnitTests.Auctions;

public sealed class AuctionWithManualSelectionTests
{
    [Fact]
    public void CanClaimAuctionItem()
    {
        // arrange
        Auction auction = new Auction(10, Auction.SelectionType.Manual);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        var commandResult = auction.Claim(
            new Auction.ClaimCommand(userId, claimedQuantity));

        // assert
        using (new AssertionScope())
        {
            commandResult.Should().NotBeNull();
            commandResult.ClaimedById.Should().Be(userId);
            commandResult.ClaimedQuantity.Should().Be(claimedQuantity);
        }
    }
    
    [Fact]
    public void CannotClaimOnSameAuctionTwiceByOneClaimer()
    {
        // arrange
        Auction auction = new Auction(10, Auction.SelectionType.Manual);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        auction.Claim(new Auction.ClaimCommand(userId, claimedQuantity));
        Action act = () => auction.Claim(new Auction.ClaimCommand(userId, claimedQuantity));
        
        // assert
        act.Should().Throw<DomainException>()
            .WithMessage(AuctionExceptions.SameUserCannotCreateTwoClaimsOnOneAuction.Message);
    }

    [Fact]
    public void AuctionItemClaimCanBeReservedManually()
    {
        // arrange
        Auction auction = new Auction(10, Auction.SelectionType.Manual);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        auction.Claim(new Auction.ClaimCommand(userId, claimedQuantity));
        AuctionEvent.ItemsReserved? commandResult = auction.Reserve(new Auction.ReserveCommand(userId));

        // assert
        using (new AssertionScope())
        {
            commandResult.Should().NotBeNull();
            commandResult.ClaimedById.Should().Be(userId);
        }
    }
    
    [Fact]
    public void CannotReserveIfThereIsNoCorrespondingClaimExists()
    {
        // arrange
        Auction auction = new Auction(10, Auction.SelectionType.Manual);
        Guid userId = Guid.NewGuid();

        // act
        Action act = () => auction.Reserve(new Auction.ReserveCommand(userId));
        
        // assert
        act.Should().Throw<DomainException>()
            .WithMessage(AuctionExceptions.CannotReserveIfThereIsNoClaimReferenced.Message);
    }

    [Fact]
    public void CanHandOverClaimedItems()
    {
        // arrange
        int initialQuantity = 10;
        Auction auction = new Auction(initialQuantity, Auction.SelectionType.Manual);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        auction.Claim(new Auction.ClaimCommand(userId, claimedQuantity));
        auction.Reserve(new Auction.ReserveCommand(userId));
        AuctionEvent.ItemsHandedOver? commandResult = auction.HandOver(new Auction.HandOverCommand(userId));

        // assert
        using (new AssertionScope())
        {
            commandResult.Should().NotBeNull();
            commandResult.HandedOverQuantity.Should().Be(claimedQuantity);
            commandResult.AvailableQuantity.Should().Be(initialQuantity - claimedQuantity);
        }
    }
}