using Freethings.Offers.Application.Entities;
using Freethings.Offers.Application.Entities.ValueObjects;
using Freethings.Offers.Application.Repositories;
using Freethings.Shared.Infrastructure;
using MediatR;

namespace Freethings.Offers.Application.Commands;

public sealed record EditOfferCommand(
    Guid UserId,
    Guid OfferId,
    string Title,
    string Description) : IRequest<Result>;

internal sealed class EditOfferHandler : IRequestHandler<EditOfferCommand, Result>
{
    private readonly IOfferRepository _repository;
    public EditOfferHandler(IOfferRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(EditOfferCommand request, CancellationToken cancellationToken)
    {
        Offer offer = await _repository.GetAsync(request.OfferId, cancellationToken);

        if (offer is null)
        {
            return Result.Failure("Offer not found");
        }
        
        offer.Title = OfferTitle.Create(request.Title);
        offer.Description = OfferDescription.Create(request.Description);
        
        await _repository.UpdateAsync(offer, cancellationToken);
        
        return Result.Success();
    }
}