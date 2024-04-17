using Freethings.Auctions.Domain;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Abstractions.Messaging;

namespace Freethings.Auctions.Application.Commands;

public sealed record ChangeAvailableItemsQuantityCommand(
    Guid AuctionId,
    Guid UserId,
    int Quantity) : IRequest<BusinessResult>;

internal sealed class ChangeAvailableItemsQuantityHandler
    : IRequestHandler<ChangeAvailableItemsQuantityCommand, BusinessResult>
{
    private readonly IAggregateRootRepository<AuctionAggregate> _repository;
    private readonly IEventBus _eventBus;
    private readonly ICurrentTime _currentTime;
    
    public Task<BusinessResult> Handle(ChangeAvailableItemsQuantityCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}