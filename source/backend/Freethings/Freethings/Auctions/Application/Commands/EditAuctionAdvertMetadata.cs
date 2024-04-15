using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Repositories;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.Auctions.Application.Commands;

public sealed record EditAuctionAdvertMetadataCommand(
    Guid UserId,
    Guid AuctionId,
    string Title,
    string Description) : IRequest<BusinessResult>;

internal sealed class EditAuctionAdverMetadatatHandler : IRequestHandler<EditAuctionAdvertMetadataCommand, BusinessResult>
{
    private readonly IAuctionAdvertRepository _repository;
    public EditAuctionAdverMetadatatHandler(IAuctionAdvertRepository repository)
    {
        _repository = repository;
    }

    public async Task<BusinessResult> Handle(EditAuctionAdvertMetadataCommand request, CancellationToken cancellationToken)
    {
        AuctionAdvert auctionAdvert = await _repository.GetAsync(request.AuctionId, cancellationToken);

        if (auctionAdvert is null)
        {
            return BusinessResult.Failure("Offer not found");
        }
        
        // TODO: Implement
            
        await _repository.UpdateAsync(auctionAdvert, cancellationToken);
        
        return BusinessResult.Success();
    }
}