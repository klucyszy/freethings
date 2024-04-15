using Freethings.Auctions.Domain.Repositories;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.Auctions.Application.Commands;

public sealed record RemoveAuctionAdvertCommand(
    Guid UserId,
    Guid AuctionId) : IRequest<BusinessResult>;

internal sealed class RemoveAuctionAdvertHandler : IRequestHandler<RemoveAuctionAdvertCommand, BusinessResult>
{
    private readonly IAuctionAdvertRepository _repository;
    public RemoveAuctionAdvertHandler(IAuctionAdvertRepository repository)
    {
        _repository = repository;
    }

    public async Task<BusinessResult> Handle(RemoveAuctionAdvertCommand request, CancellationToken cancellationToken)
    {
        bool deleted = await _repository.DeleteAsync(request.AuctionId, cancellationToken);
        
        if (deleted is false)
        {
            return BusinessResult.Failure("Offer not found");
        }
        
        return BusinessResult.Success();
    }
}