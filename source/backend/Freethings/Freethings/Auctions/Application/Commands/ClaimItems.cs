using Freethings.Auctions.Application.Errors;
using Freethings.Auctions.Domain;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Messaging;
using Freethings.Shared.Infrastructure;
using MediatR;

namespace Freethings.Auctions.Application.Commands;

public sealed record ClaimItemsCommand(
    Guid AuctionId,
    Guid UserId,
    int Quantity) : IRequest<Result>;

internal sealed class ClaimItemsHandler : IRequestHandler<ClaimItemsCommand, Result>
{
    private readonly IAggregateRootRepository<Auction> _repository;
    private readonly IEventBus _eventBus;

    public ClaimItemsHandler(IAggregateRootRepository<Auction> repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public async Task<Result> Handle(ClaimItemsCommand request, CancellationToken cancellationToken)
    {
        Auction aggregate = await _repository.GetAsync(request.AuctionId, cancellationToken);

        if (aggregate is null)
        {
            return Result.Failure(AuctionErrorDefinition.AuctionNotFound);
        }

        Auction.ClaimCommand command = new Auction.ClaimCommand(
            request.UserId,
            request.Quantity);
        
        aggregate.Claim(command);
        
        List<IDomainEvent> domainEvents = await _repository
            .SaveAsync(aggregate, cancellationToken);

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _eventBus.PublishAsync(domainEvent, cancellationToken);
        }
        
        return Result.Success();
    }
}