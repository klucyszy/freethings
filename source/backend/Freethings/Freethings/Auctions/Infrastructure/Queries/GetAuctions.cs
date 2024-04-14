using Freethings.Auctions.Infrastructure.Persistence;
using Freethings.Auctions.Infrastructure.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Auctions.Infrastructure.Queries;

public sealed record GetAuctionsQuery(
    Guid UserId)
    : IRequest<List<AuctionDto>>;

internal sealed class GetOffersHandler : IRequestHandler<GetAuctionsQuery, List<AuctionDto>>
{
    private readonly AuctionsContext _context;

    public GetOffersHandler(AuctionsContext context)
    {
        _context = context;
    }

    public async Task<List<AuctionDto>> Handle(GetAuctionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Auctions
            .Where(q => q.UserId == request.UserId)
            .Select(o => new AuctionDto(
                o.Id,
                o.Title.Value,
                o.Description.Value,
                o.State.ToString()))
            .ToListAsync(cancellationToken);
    }
}