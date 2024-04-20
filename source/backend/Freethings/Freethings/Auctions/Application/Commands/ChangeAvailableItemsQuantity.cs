using Freethings.Auctions.Application.Errors;
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

    public ChangeAvailableItemsQuantityHandler(IAggregateRootRepository<AuctionAggregate> repository,
        IEventBus eventBus, ICurrentTime currentTime)
    {
        _repository = repository;
        _eventBus = eventBus;
        _currentTime = currentTime;
    }

    public async Task<BusinessResult> Handle(ChangeAvailableItemsQuantityCommand request, CancellationToken cancellationToken)
    {
        Quantity quantity = Quantity.Create(request.Quantity);
        
        AuctionAggregate aggregate = await _repository.GetAsync(request.AuctionId, cancellationToken);

        if (aggregate is null)
        {
            return BusinessResult.Failure(AuctionErrorDefinition.AuctionNotFound);
        }
        
        aggregate.ChangeAvailableQuantity(quantity, _currentTime.UtcNow());
        
        List<IDomainEvent> domainEvents = await _repository
            .SaveAsync(aggregate, cancellationToken);

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _eventBus.PublishAsync(domainEvent, cancellationToken);
        }
        
        return BusinessResult.Success();
    }
}