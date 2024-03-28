using Freethings.Offers.Application.Entities;
using Freethings.Offers.Application.Entities.ValueObjects;
using Freethings.Offers.Application.Repositories;
using MediatR;

namespace Freethings.Offers.Application.Commands;

public sealed record AddOfferCommand(
    Guid UserId,
    string Title,
    string Description,
    int Quantity) : IRequest<Guid>;

internal sealed class AddOfferHandler : IRequestHandler<AddOfferCommand, Guid>
{
    private readonly IOfferRepository _repository;
    public AddOfferHandler(IOfferRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(AddOfferCommand request, CancellationToken cancellationToken)
    {
        Offer offer = new Offer(
            request.UserId,
            OfferTitle.Create(request.Title),
            OfferDescription.Create(request.Description),
            Offer.SelectionType.Manual,
            request.Quantity);

        Offer created = await _repository.AddAsync(offer, cancellationToken);

        return created.Id;
    }
}