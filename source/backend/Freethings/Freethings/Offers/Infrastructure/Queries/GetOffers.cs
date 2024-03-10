using Freethings.Offers.Infrastructure.Persistence;
using Freethings.Offers.Infrastructure.Queries.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Offers.Infrastructure.Queries;

public sealed record GetOffersQuery(
    Guid UserId)
    : IRequest<List<OfferDto>>;

internal sealed class GetOffersHandler : IRequestHandler<GetOffersQuery, List<OfferDto>>
{
    private readonly OffersContext _context;

    public GetOffersHandler(OffersContext context)
    {
        _context = context;
    }

    public async Task<List<OfferDto>> Handle(GetOffersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Offers
            .Where(q => q.UserId == request.UserId)
            .Select(o => new OfferDto(
                o.Id,
                o.Title.Value,
                o.Description.Value,
                o.State.ToString()))
            .ToListAsync(cancellationToken);
    }
}