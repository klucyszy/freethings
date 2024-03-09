using Freethings.Offers.Domain.Entities;
using Freethings.Offers.Domain.Repositories;
using Freethings.Offers.Domain.ValueObjects;
using Freethings.Shared;
using MediatR;

namespace Freethings.Offers.Application.Commands.EditOffer;

public sealed record EditOfferCommand(
    Guid UserId,
    Guid OfferId,
    string Title,
    string Description) : IRequest<Result>;

public sealed class Handler : IRequestHandler<EditOfferCommand, Result>
{
    private readonly IOfferRepository _repository;
    public Handler(IOfferRepository repository)
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