using Freethings.Offers.Domain.Entities;
using Freethings.Offers.Infrastructure.Persistence;
using Freethings.Offers.Infrastructure.Queries.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Offers.Infrastructure.Queries.GetOffer;

public sealed record GetOfferQuery(
    Guid UserId,
    Guid OfferId)
    : IRequest<OfferDto>;

internal sealed class Handler : IRequestHandler<GetOfferQuery, OfferDto>
{
    private readonly OffersContext _context;

    public Handler(OffersContext context)
    {
        _context = context;
    }

    public async Task<OfferDto> Handle(GetOfferQuery request, CancellationToken cancellationToken)
    {
        return await _context.Offers
            .Where(q => q.UserId == request.UserId && q.Id == request.OfferId)
            .Select(o => new OfferDto(
                o.Id,
                o.Title.Value,
                o.Description.Value))
            .FirstOrDefaultAsync(cancellationToken);
    }
}