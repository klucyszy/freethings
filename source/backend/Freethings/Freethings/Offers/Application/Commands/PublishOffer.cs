using Freethings.Contracts;
using Freethings.Offers.Domain.Entities;
using Freethings.Offers.Domain.Repositories;
using Freethings.Offers.Domain.ValueObjects;
using Freethings.Shared;
using Freethings.Shared.Messaging;
using MediatR;

namespace Freethings.Offers.Application.Commands;

public sealed record PublishOfferCommand(
    Guid UserId,
    Guid OfferId) : IRequest<Result>;

public sealed class PublishOfferHandler : IRequestHandler<PublishOfferCommand, Result>
{
    private readonly IOfferRepository _repository;
    private readonly IEventBus _bus;
    public PublishOfferHandler(IOfferRepository repository, IEventBus bus)
    {
        _repository = repository;
        _bus = bus;
    }

    public async Task<Result> Handle(PublishOfferCommand request, CancellationToken cancellationToken)
    {
        Offer offer = await _repository.GetAsync(request.OfferId, cancellationToken);

        if (offer is null)
        {
            return Result.Failure("Offer not found");
        }
        
        offer.Publish();
        
        await _repository.UpdateAsync(offer, cancellationToken);

        await _bus.PublishAsync(new OfferPublished(), cancellationToken);
        
        return Result.Success();
    }
}