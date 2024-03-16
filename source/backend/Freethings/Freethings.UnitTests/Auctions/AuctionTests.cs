using FluentAssertions;
using FluentAssertions.Execution;
using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Exceptions;
using Freethings.Contracts.Events;
using Freethings.Offers.Domain.Entities;
using Freethings.Shared.Exceptions;

namespace Freethings.UnitTests.Auctions;

public sealed class AuctionTests
{
    [Fact]
    public void UserCanClaimAuctionItem()
    {
        // arrange
        Auction auction = new Auction(10, Auction.SelectionType.Manual);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        var commandResult = auction.Claim(
            new Auction.ClaimItemsCommand(userId, claimedQuantity));

        // assert
        using (new AssertionScope())
        {
            commandResult.Should().NotBeNull();
            commandResult.ClaimedById.Should().Be(userId);
            commandResult.ClaimedQuantity.Should().Be(claimedQuantity);
        }
    }
    
    [Fact]
    public void UserCannotClaimOnSameAuctionTwice()
    {
        // arrange
        Auction auction = new Auction(10, Auction.SelectionType.Manual);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        auction.Claim(new Auction.ClaimItemsCommand(userId, claimedQuantity));
        Action act = () => auction.Claim(new Auction.ClaimItemsCommand(userId, claimedQuantity));
        
        // assert
        act.Should().Throw<DomainException>()
            .WithMessage(AuctionExceptions.SameUserCannotCreateTwoClaimsOnOneAuction.Message);
    }

    [Fact]
    public void AuctionItemClaimCanBeSelectedManually()
    {
        // arrange
        Auction auction = new Auction(10, Auction.SelectionType.Manual);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        auction.Claim(new Auction.ClaimItemsCommand(userId, claimedQuantity));
        AuctionEvent.ItemsReserved? commandResult = auction.ReserveItems(new Auction.ReserveItemsCommand(userId));

        // assert
        using (new AssertionScope())
        {
            commandResult.Should().NotBeNull();
            commandResult.ClaimedById.Should().Be(userId);
        }
    }
    
    [Fact]
    public void CannotReserveItemsIfNoCorrespondingClaimExists()
    {
        // arrange
        Auction auction = new Auction(10, Auction.SelectionType.Manual);
        Guid userId = Guid.NewGuid();

        // act
        Action act = () => auction.ReserveItems(new Auction.ReserveItemsCommand(userId));
        
        // assert
        act.Should().Throw<DomainException>()
            .WithMessage(AuctionExceptions.CannotReserveItemsIfThereIsNoClaimReferenced.Message);
    }
}