using Freethings.Offers.Application.Repositories;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.Offers.Application.Commands;

public sealed record RemoveOfferCommand(
    Guid UserId,
    Guid OfferId) : IRequest<BusinessResult>;

internal sealed class RemoveOfferHandler : IRequestHandler<RemoveOfferCommand, BusinessResult>
{
    private readonly IOfferRepository _repository;
    public RemoveOfferHandler(IOfferRepository repository)
    {
        _repository = repository;
    }

    public async Task<BusinessResult> Handle(RemoveOfferCommand request, CancellationToken cancellationToken)
    {
        bool deleted = await _repository.DeleteAsync(request.OfferId, cancellationToken);
        
        if (deleted is false)
        {
            return BusinessResult.Failure("Offer not found");
        }
        
        return BusinessResult.Success();
    }
}