using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Repositories;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Abstractions.Messaging;

namespace Freethings.Auctions.Application.Commands;

public sealed record EditAuctionAdvertMetadataCommand(
    Guid UserId,
    Guid AuctionId,
    string? Title,
    string? Description) : IRequest<BusinessResult>;

internal sealed class EditAuctionAdverMetadatatHandler : IRequestHandler<EditAuctionAdvertMetadataCommand, BusinessResult>
{
    private readonly IAuctionAdvertRepository _repository;
    private readonly IEventBus _eventBus;
    public EditAuctionAdverMetadatatHandler(IAuctionAdvertRepository repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public async Task<BusinessResult> Handle(EditAuctionAdvertMetadataCommand request, CancellationToken cancellationToken)
    {
        AuctionAdvert auctionAdvert = await _repository.GetAsync(request.AuctionId, cancellationToken);

        if (auctionAdvert is null)
        {
            return BusinessResult.Failure("§Auction not found");
        }
        
        // handle patching
        Title title = request.Title is not null
            ? Title.Create(request.Title)
            : auctionAdvert.Title;
        Description description = request.Description is not null
            ? Description.Create(request.Description)
            : auctionAdvert.Description;
        
        auctionAdvert.ChangeAuctionMetadata(
            title,
            description);
            
        List<IDomainEvent> domainEvents = await _repository
            .UpdateAsync(auctionAdvert, cancellationToken);
        
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _eventBus.PublishAsync(domainEvent, cancellationToken);
        }
        return BusinessResult.Success();
    }
}