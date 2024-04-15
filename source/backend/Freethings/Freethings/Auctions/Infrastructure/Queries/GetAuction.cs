using Freethings.Auctions.Infrastructure.Persistence;
using Freethings.Auctions.Infrastructure.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Auctions.Infrastructure.Queries;

public sealed record GetAuctionQuery(
    Guid UserId,
    Guid AuctionId)
    : IRequest<AuctionDto>;

internal sealed class GetAuctionHandler : IRequestHandler<GetAuctionQuery, AuctionDto>
{
    private readonly AuctionsContext _context;

    public GetAuctionHandler(AuctionsContext context)
    {
        _context = context;
    }

    public async Task<AuctionDto> Handle(GetAuctionQuery request, CancellationToken cancellationToken)
    {
        return await _context.Auctions
            .Where(q => q.UserId == request.UserId && q.Id == request.AuctionId)
            .Select(o => new AuctionDto(
                o.Id,
                o.Title.Value,
                o.Description.Value,
                o.State.ToString()))
            .FirstOrDefaultAsync(cancellationToken);
    }
}