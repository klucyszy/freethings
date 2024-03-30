using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Repositories;
using Freethings.Contracts.Events;
using Freethings.Shared.Abstractions.Messaging;

namespace Freethings.Auctions.Application.Commands;

public sealed record CreateAuctionAdvertCommand(
    Guid UserId,
    AuctionType Type,
    string Title,
    string Description,
    int Quantity) : IRequest<Guid>;

internal sealed class CreateAuctionAdvertHandler : IRequestHandler<CreateAuctionAdvertCommand, Guid>
{
    private readonly IAuctionAdvertRepository _repository;
    private readonly ICurrentTime _currentTime;
    private readonly IEventBus _eventBus;
    
    public CreateAuctionAdvertHandler(IAuctionAdvertRepository repository, ICurrentTime currentTime, IEventBus eventBus)
    {
        _repository = repository;
        _currentTime = currentTime;
        _eventBus = eventBus;
    }

    public async Task<Guid> Handle(CreateAuctionAdvertCommand request, CancellationToken cancellationToken)
    {
        Quantity quantity = Quantity.Create(request.Quantity);
        Title title = Title.Create(request.Title);
        Description description = Description.Create(request.Description);
        
        AuctionAdvert auctionAdvert = new(
            request.UserId,
            quantity,
            request.Type,
            title,
            description,
            _currentTime.UtcNow());

        AuctionAdvert created = await _repository.AddAsync(auctionAdvert, cancellationToken);

        await _eventBus.PublishAsync(new AuctionEvent.AdvertCreated(
            created.Id,
            created.UserId,
            created.Title.Value,
            created.Description.Value,
            _currentTime.UtcNow()), cancellationToken);

        return created.Id;
    }
}