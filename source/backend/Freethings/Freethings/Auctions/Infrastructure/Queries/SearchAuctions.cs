using Freethings.Auctions.Domain;
using Freethings.Auctions.Infrastructure.Persistence;
using Freethings.Auctions.Infrastructure.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Auctions.Infrastructure.Queries;

public sealed record SearchAuctionsQuery(
    int Page,
    int ElementsPerPage,
    string SearchText,
    string Category)
    : IRequest<List<AuctionDto>>;

internal sealed class SearchAuctionsHandler : IRequestHandler<SearchAuctionsQuery, List<AuctionDto>>
{
    private readonly AuctionsContext _context;

    public SearchAuctionsHandler(AuctionsContext context)
    {
        _context = context;
    }

    public async Task<List<AuctionDto>> Handle(SearchAuctionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Auctions
            .Where(q => q.State == AuctionState.Published
                && (string.IsNullOrEmpty(request.SearchText)
                    || EF.Functions.Like(q.Title.Value, $"%{request.SearchText}%")
                    || EF.Functions.Like(q.Description.Value, $"%{request.SearchText}%")))
            .Select(o => new AuctionDto(
                o.Id,
                o.Title.Value,
                o.Description.Value,
                o.State.ToString(),
                o.Quantity.Value,
                0))
            .Take(request.ElementsPerPage)
            .Skip(request.ElementsPerPage * (request.Page - 1))
            .ToListAsync(cancellationToken);
    }
}