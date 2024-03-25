using Freethings.Auctions.Application.Errors;
using Freethings.Auctions.Domain;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Infrastructure;
using MediatR;

namespace Freethings.Auctions.Application.Commands;

public sealed record ClaimItemCommand(
    Guid AuctionId,
    Guid UserId,
    int Quantity) : IRequest<Result>;

internal sealed class ClaimItemHandler : IRequestHandler<ClaimItemCommand, Result>
{
    private readonly IAggregateRootRepository<Auction> _repository;

    public ClaimItemHandler(IAggregateRootRepository<Auction> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(ClaimItemCommand request, CancellationToken cancellationToken)
    {
        Auction auction = await _repository.GetAsync(request.AuctionId, cancellationToken);

        if (auction is null)
        {
            return Result.Failure(AuctionErrorDefinition.AuctionNotFound);
        }

        Auction.ClaimCommand command = new Auction.ClaimCommand(
            request.UserId,
            request.Quantity);
        
        auction.Claim(command);
        
        await _repository.SaveAsync(auction, cancellationToken);

        return Result.Success();
    }
}