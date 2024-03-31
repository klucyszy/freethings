using Freethings.Auctions.Application.Errors;
using Freethings.Auctions.Domain;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Abstractions.Messaging;

namespace Freethings.Auctions.Application.Commands;

public sealed record ReserveClaimedItemsCommand(
    Guid AuctionId,
    Guid ClaimId,
    int Quantity,
    bool TriggeredByUser) : IRequest<BusinessResult>;

internal sealed class ReserveClaimedItemsHandler : IRequestHandler<ReserveClaimedItemsCommand, BusinessResult>
{
    private readonly IAggregateRootRepository<AuctionAggregate> _repository;
    private readonly IEventBus _eventBus;

    public ReserveClaimedItemsHandler(IAggregateRootRepository<AuctionAggregate> repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public async Task<BusinessResult> Handle(ReserveClaimedItemsCommand request, CancellationToken cancellationToken)
    {
        AuctionAggregate auctionAggregate = await _repository
            .GetAsync(request.AuctionId, cancellationToken);

        if (auctionAggregate is null)
        {
            return BusinessResult.Failure(AuctionErrorDefinition.AuctionNotFound);
        }
        
        AuctionAggregate.ReserveCommand command = new(request.ClaimId, request.TriggeredByUser);
        
        BusinessResult reserveBusinessResult = auctionAggregate.Reserve(command);
        
        if (!reserveBusinessResult.IsSuccess)
        {
            return reserveBusinessResult;
        }
        
        List<IDomainEvent> domainEvents = await _repository
            .SaveAsync(auctionAggregate, cancellationToken);

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _eventBus.PublishAsync(domainEvent, cancellationToken);
        }
        
        return BusinessResult.Success();
    }
}