using Freethings.Auctions.Application.Errors;
using Freethings.Auctions.Domain;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Abstractions.Messaging;

namespace Freethings.Auctions.Application.Commands;

public sealed record ClaimItemsCommand(
    Guid AuctionId,
    Guid UserId,
    int Quantity) : IRequest<BusinessResult>;

internal sealed class ClaimItemsHandler : IRequestHandler<ClaimItemsCommand, BusinessResult>
{
    private readonly IAggregateRootRepository<AuctionAggregate> _repository;
    private readonly IEventBus _eventBus;
    private readonly ICurrentTime _currentTime;

    public ClaimItemsHandler(
        IAggregateRootRepository<AuctionAggregate> repository,
        IEventBus eventBus,
        ICurrentTime currentTime)
    {
        _repository = repository;
        _eventBus = eventBus;
        _currentTime = currentTime;
    }

    public async Task<BusinessResult> Handle(ClaimItemsCommand request, CancellationToken cancellationToken)
    {
        AuctionAggregate aggregate = await _repository.GetAsync(request.AuctionId, cancellationToken);

        if (aggregate is null)
        {
            return BusinessResult.Failure(AuctionErrorDefinition.AuctionNotFound);
        }

        AuctionAggregate.ClaimCommand command = new AuctionAggregate.ClaimCommand(
            request.UserId,
            Quantity.Create(request.Quantity));

        aggregate.Claim(command, _currentTime.UtcNow());
        
        List<IDomainEvent> domainEvents = await _repository
            .SaveAsync(aggregate, cancellationToken);

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _eventBus.PublishAsync(domainEvent, cancellationToken);
        }
        
        return BusinessResult.Success();
    }
}