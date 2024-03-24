using Freethings.Auctions.Domain;
using Freethings.Shared.Abstractions.Domain;
using MediatR;

namespace Freethings.Auctions.Application.Commands;

public sealed record ClaimItemCommand(
    Guid AuctionId,
    Guid UserId,
    int Quantity) : IRequest<Guid>;

internal sealed class ClaimItemHandler : IRequestHandler<ClaimItemCommand, Guid>
{
    private readonly IAggregateRootRepository<Auction> _repository;

    public ClaimItemHandler(IAggregateRootRepository<Auction> repository)
    {
        _repository = repository;
    }

    public Task<Guid> Handle(ClaimItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}