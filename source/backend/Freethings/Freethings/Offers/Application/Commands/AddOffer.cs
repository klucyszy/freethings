using Freethings.Offers.Domain.Entities;
using Freethings.Offers.Domain.Repositories;
using Freethings.Offers.Domain.ValueObjects;
using MediatR;

namespace Freethings.Offers.Application.Commands;

public sealed record AddOfferCommand(
    Guid UserId,
    string Title,
    string Description,
    int Quantity) : IRequest<Guid>;

public sealed class AddOfferHandler : IRequestHandler<AddOfferCommand, Guid>
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