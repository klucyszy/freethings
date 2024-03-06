using Freethings.Offers.Domain.Entities;
using Freethings.Offers.Domain.Repositories;
using Freethings.Offers.Domain.ValueObjects;
using MediatR;

namespace Freethings.Offers.Application.Commands.AddOffer;

public sealed record AddOfferCommand(string Title, string Description) : IRequest<Guid>;

public sealed class Handler : IRequestHandler<AddOfferCommand, Guid>
{
    private readonly IOfferRepository _repository;
    public Handler(IOfferRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(AddOfferCommand request, CancellationToken cancellationToken)
    {
        Offer offer = new Offer(
            Guid.NewGuid(),
            OfferTitle.Create(request.Title),
            OfferDescription.Create(request.Description));

        Offer created = await _repository.AddAsync(offer, cancellationToken);

        return created.Id;
    }
}