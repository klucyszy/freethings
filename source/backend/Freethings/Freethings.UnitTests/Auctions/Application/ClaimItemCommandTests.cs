using FluentAssertions;
using FluentAssertions.Execution;
using Freethings.Auctions.Application.Commands;
using Freethings.Auctions.Application.Errors;
using Freethings.Auctions.Domain;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Messaging;
using Freethings.Shared.Infrastructure;
using Freethings.Shared.Infrastructure.Time;
using NSubstitute;

namespace Freethings.UnitTests.Auctions.Application;

public sealed class ClaimItemCommandTests
{
    private async Task<Result> Act(ClaimItemsCommand command)
        => await _handler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task WhenAuctionDoesNotExists_ThenNotFoundReturned()
    {
        // Arrange
        ClaimItemsCommand command = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            5);

        // Act
        Result result = await Act(command);

        // Assert
        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().BeEquivalentTo(AuctionErrorDefinition.AuctionNotFound);
        }
    }
    
    [Fact]
    public async Task WhenAuctionExists_ItemClaimed()
    {
        // Arrange
        ClaimItemsCommand command = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            5);
        
        _repository
            .GetAsync(command.AuctionId, Arg.Any<CancellationToken>())
            .Returns(AuctionFixtures.CreateAuction(AuctionType.Manual, 10));
        _repository
            .SaveAsync(
                Arg.Is<AuctionAggregate>(p => p.DomainEvents.Count > 0),
                Arg.Any<CancellationToken>())
            .Returns(new List<IDomainEvent>());
        
        // Act
        Result result = await Act(command);

        // Assert
        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
        }
    }

    #region Arrange

    private readonly ClaimItemsHandler _handler;

    private readonly IAggregateRootRepository<AuctionAggregate>
        _repository = Substitute.For<IAggregateRootRepository<AuctionAggregate>>();
    private readonly IEventBus _eventBus = Substitute.For<IEventBus>();
    private readonly ICurrentTime _currentTime = new CurrentTime();
    
    public ClaimItemCommandTests()
    {
        _handler = new ClaimItemsHandler(
            _repository,
            _eventBus,
            _currentTime);
    }

    #endregion
}