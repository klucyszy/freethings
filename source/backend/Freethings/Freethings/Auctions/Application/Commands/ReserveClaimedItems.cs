using Freethings.Auctions.Application.Errors;
using Freethings.Auctions.Domain;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Messaging;
using Freethings.Shared.Infrastructure;
using MediatR;

namespace Freethings.Auctions.Application.Commands;

public sealed record ReserveClaimedItemsCommand(
    Guid AuctionId,
    Guid ClaimId,
    int Quantity,
    bool TriggeredByUser) : IRequest<Result>;

internal sealed class ReserveClaimedItemsHandler : IRequestHandler<ReserveClaimedItemsCommand, Result>
{
    private readonly IAggregateRootRepository<AuctionAggregate> _repository;
    private readonly IEventBus _eventBus;

    public ReserveClaimedItemsHandler(IAggregateRootRepository<AuctionAggregate> repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public async Task<Result> Handle(ReserveClaimedItemsCommand request, CancellationToken cancellationToken)
    {
        AuctionAggregate auctionAggregate = await _repository
            .GetAsync(request.AuctionId, cancellationToken);

        if (auctionAggregate is null)
        {
            return Result.Failure(AuctionErrorDefinition.AuctionNotFound);
        }
        
        AuctionAggregate.ReserveCommand command = new AuctionAggregate.ReserveCommand(request.ClaimId, request.TriggeredByUser);
        
        auctionAggregate.Reserve(command);
        
        List<IDomainEvent> domainEvents = await _repository
            .SaveAsync(auctionAggregate, cancellationToken);

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _eventBus.PublishAsync(domainEvent, cancellationToken);
        }
        
        return Result.Success();
    }
}