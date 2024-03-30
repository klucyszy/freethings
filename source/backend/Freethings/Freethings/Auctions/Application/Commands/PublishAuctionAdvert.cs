using Freethings.Auctions.Application.Errors;
using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Repositories;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Messaging;
using Freethings.Shared.Infrastructure;

namespace Freethings.Auctions.Application.Commands;

public sealed record PublishAuctionAdvertCommand(
    Guid AuctionAdvertId) : IRequest<Result>;

internal sealed class PublishAuctionAdvertHandler : IRequestHandler<PublishAuctionAdvertCommand, Result>
{
    private readonly IAuctionAdvertRepository _repository;
    private readonly IEventBus _eventBus;
    private readonly ICurrentTime _currentTime;
    
    public PublishAuctionAdvertHandler(IAuctionAdvertRepository repository, IEventBus eventBus, ICurrentTime currentTime)
    {
        _repository = repository;
        _eventBus = eventBus;
        _currentTime = currentTime;
    }

    public async Task<Result> Handle(PublishAuctionAdvertCommand request, CancellationToken cancellationToken)
    {
        AuctionAdvert auctionAdvert = await _repository.GetAsync(request.AuctionAdvertId, cancellationToken);

        if (auctionAdvert is null)
        {
            return Result.Failure(AuctionErrorDefinition.AuctionNotFound);
        }
        
        auctionAdvert.Publish(_currentTime.UtcNow());
        
        List<IDomainEvent> domainEvents = await _repository.UpdateAsync(auctionAdvert, cancellationToken);

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _eventBus.PublishAsync(domainEvent, cancellationToken);
        }
        
        return Result.Success();
    }
}