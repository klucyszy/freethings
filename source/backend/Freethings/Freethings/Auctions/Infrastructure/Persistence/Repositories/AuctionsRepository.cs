using Freethings.Auctions.Domain;
using Freethings.Auctions.Infrastructure.Persistence.Entities;
using Freethings.Auctions.Infrastructure.Persistence.Mappers;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Infrastructure.Time;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Auctions.Infrastructure.Persistence.Repositories;

internal sealed class AuctionsRepository : IAggregateRootRepository<Auction>
{
    private readonly AuctionsContext _context;
    private readonly ICurrentTime _currentTime;

    public AuctionsRepository(AuctionsContext context, ICurrentTime currentTime)
    {
        _context = context;
        _currentTime = currentTime;
    }

    public async Task<Auction> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Auctions
            .Include(p => p.Claims)
            .Where(p => p.Id == id)
            .Select(p => p.ToAggregate(_currentTime))
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task SaveAsync(Auction aggregateRoot, CancellationToken cancellationToken = default)
    {
        AuctionEntity entity = await _context.Auctions
            .FindAsync(new [] { aggregateRoot.Id }, cancellationToken);

        aggregateRoot.ToEntity(entity);
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}