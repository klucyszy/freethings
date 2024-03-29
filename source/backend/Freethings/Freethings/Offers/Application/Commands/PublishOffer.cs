using Freethings.Offers.Application.Entities;
using Freethings.Offers.Application.Repositories;
using Freethings.Shared.Abstractions.Messaging;
using Freethings.Shared.Infrastructure;

namespace Freethings.Offers.Application.Commands;

public sealed record PublishOfferCommand(
    Guid UserId,
    Guid OfferId) : IRequest<Result>;

internal sealed class PublishOfferHandler : IRequestHandler<PublishOfferCommand, Result>
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

        // await _bus.PublishAsync(new OfferEvent.Published(), cancellationToken);
        //
        return Result.Success();
    }
}