using FluentAssertions;
using FluentAssertions.Execution;
using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Exceptions;
using Freethings.Contracts.Events;
using Freethings.Shared.Exceptions;

namespace Freethings.UnitTests.Auctions;

public sealed class AuctionWithManualSelectionTests
{
    [Fact]
    public void CanClaimAuctionItem()
    {
        // arrange
        ManualAuction manualAuction = CreateManualAuction(10);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        var commandResult = manualAuction.Claim(
            new ManualAuction.ClaimCommand(userId, claimedQuantity));

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
        ManualAuction manualAuction = CreateManualAuction(10);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        manualAuction.Claim(new ManualAuction.ClaimCommand(userId, claimedQuantity));
        Action act = () => manualAuction.Claim(new ManualAuction.ClaimCommand(userId, claimedQuantity));
        
        // assert
        act.Should().Throw<DomainException>()
            .WithMessage(AuctionExceptions.SameUserCannotCreateTwoClaimsOnOneAuction.Message);
    }

    [Fact]
    public void AuctionItemClaimCanBeReservedManually()
    {
        // arrange
        ManualAuction manualAuction = CreateManualAuction(10);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        manualAuction.Claim(new ManualAuction.ClaimCommand(userId, claimedQuantity));
        AuctionEvent.ItemsReserved? commandResult = manualAuction.Reserve(new ManualAuction.ReserveCommand(userId));

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
        ManualAuction manualAuction = CreateManualAuction(10);
        Guid userId = Guid.NewGuid();

        // act
        Action act = () => manualAuction.Reserve(new ManualAuction.ReserveCommand(userId));
        
        // assert
        act.Should().Throw<DomainException>()
            .WithMessage(AuctionExceptions.CannotReserveIfThereIsNoClaimReferenced.Message);
    }

    [Fact]
    public void CanHandOverClaimedItems()
    {
        // arrange
        int initialQuantity = 10;
        ManualAuction manualAuction = CreateManualAuction(initialQuantity);
        Guid userId = Guid.NewGuid();
        int claimedQuantity = 3;

        // act
        manualAuction.Claim(new ManualAuction.ClaimCommand(userId, claimedQuantity));
        manualAuction.Reserve(new ManualAuction.ReserveCommand(userId));
        AuctionEvent.ItemsHandedOver? commandResult = manualAuction.HandOver(new ManualAuction.HandOverCommand(userId));

        // assert
        using (new AssertionScope())
        {
            commandResult.Should().NotBeNull();
            commandResult.HandedOverQuantity.Should().Be(claimedQuantity);
            commandResult.AvailableQuantity.Should().Be(initialQuantity - claimedQuantity);
        }
    }
    
    private ManualAuction CreateManualAuction(int availableQuantity)
    {
        Auction auction = AuctionFactory.Create(
            Auction.Type.Manual,
            new List<AuctionClaim>(),
            10);
        
        return (auction as ManualAuction)!;
    }
}