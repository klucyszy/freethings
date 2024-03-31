using Freethings.Offers.Application.Entities;
using Freethings.Offers.Application.Entities.ValueObjects;
using Freethings.Offers.Application.Repositories;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.Offers.Application.Commands;

public sealed record EditOfferCommand(
    Guid UserId,
    Guid OfferId,
    string Title,
    string Description) : IRequest<BusinessResult>;

internal sealed class EditOfferHandler : IRequestHandler<EditOfferCommand, BusinessResult>
{
    private readonly IOfferRepository _repository;
    public EditOfferHandler(IOfferRepository repository)
    {
        _repository = repository;
    }

    public async Task<BusinessResult> Handle(EditOfferCommand request, CancellationToken cancellationToken)
    {
        Offer offer = await _repository.GetAsync(request.OfferId, cancellationToken);

        if (offer is null)
        {
            return BusinessResult.Failure("Offer not found");
        }
        
        offer.Title = OfferTitle.Create(request.Title);
        offer.Description = OfferDescription.Create(request.Description);
        
        await _repository.UpdateAsync(offer, cancellationToken);
        
        return BusinessResult.Success();
    }
}