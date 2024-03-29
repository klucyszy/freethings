using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Repositories;

namespace Freethings.Auctions.Application.Commands;

public sealed record AddAuctionAdvertCommand(
    Guid UserId,
    AuctionType Type,
    string Title,
    string Description,
    int Quantity) : IRequest<Guid>;

internal sealed class AddAuctionAdvertHandler : IRequestHandler<AddAuctionAdvertCommand, Guid>
{
    private readonly IAuctionAdvertRepository _repository;
    public AddAuctionAdvertHandler(IAuctionAdvertRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(AddAuctionAdvertCommand request, CancellationToken cancellationToken)
    {
        Quantity quantity = Quantity.Create(request.Quantity);
        Title title = Title.Create(request.Title);
        Description description = Description.Create(request.Description);
        
        AuctionAdvert offer = new(
            quantity,
            request.Type,
            title,
            description);

        AuctionAdvert created = await _repository.AddAsync(offer, cancellationToken);

        return created.Id;
    }
}