using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Repositories;
using Freethings.Shared.Abstractions.Domain;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Auctions.Infrastructure.Persistence.Repositories;

internal sealed class AuctionAdvertRepository : IAuctionAdvertRepository
{
    private readonly AuctionsContext _context;

    public AuctionAdvertRepository(AuctionsContext context)
    {
        _context = context;
    }

    public async Task<AuctionAdvert> AddAsync(AuctionAdvert auctionAdvert, CancellationToken cancellationToken = default)
    {
        _context.Auctions.Add(auctionAdvert);

        await _context.SaveChangesAsync(cancellationToken);

        return auctionAdvert;
    }

    public Task<AuctionAdvert> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Auctions
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<List<IDomainEvent>> UpdateAsync(AuctionAdvert auctionAdvert, CancellationToken cancellationToken = default)
    {
        _context.Auctions.Update(auctionAdvert);
        
        List<IDomainEvent> domainEvents = auctionAdvert.DomainEvents.ToList();
        
        await _context.SaveChangesAsync(cancellationToken);
        auctionAdvert.ClearDomainEvents();

        return domainEvents;
    }
}