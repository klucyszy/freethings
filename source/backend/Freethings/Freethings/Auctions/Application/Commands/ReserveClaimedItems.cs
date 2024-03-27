using Freethings.Auctions.Application.Errors;
using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Strategies;
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
    private readonly IAggregateRootRepository<Auction> _repository;
    private readonly IEventBus _eventBus;

    public ReserveClaimedItemsHandler(IAggregateRootRepository<Auction> repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public async Task<Result> Handle(ReserveClaimedItemsCommand request, CancellationToken cancellationToken)
    {
        Auction auction = await _repository
            .GetAsync(request.AuctionId, cancellationToken);

        if (auction is null)
        {
            return Result.Failure(AuctionErrorDefinition.AuctionNotFound);
        }
        
        Auction.ReserveCommand command = new Auction.ReserveCommand(request.ClaimId, request.TriggeredByUser);
        
        auction.Reserve(command);
        
        List<IDomainEvent> domainEvents = await _repository
            .SaveAsync(auction, cancellationToken);

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _eventBus.PublishAsync(domainEvent, cancellationToken);
        }
        
        return Result.Success();
    }
}