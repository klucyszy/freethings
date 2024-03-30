using Freethings.Auctions.Domain;
using Freethings.Auctions.Infrastructure.Persistence.Mappers;
using Freethings.Shared.Abstractions.Domain;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Auctions.Infrastructure.Persistence.Repositories;

internal sealed class AuctionAggregateRepository : IAggregateRootRepository<AuctionAggregate>
{
    private readonly AuctionsContext _context;
    private readonly ICurrentTime _currentTime;

    public AuctionAggregateRepository(AuctionsContext context, ICurrentTime currentTime)
    {
        _context = context;
        _currentTime = currentTime;
    }

    public async Task<AuctionAggregate> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Auctions
            .Include(p => p.Claims)
            .Where(p => p.Id == id && p.State == AuctionState.Published)
            .Select(p => p.ToAggregate(_currentTime))
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<List<IDomainEvent>> SaveAsync(AuctionAggregate aggregateRoot, CancellationToken cancellationToken = default)
    {
        AuctionAdvert entity = await _context.Auctions
            .FindAsync(aggregateRoot.Id, cancellationToken);

        entity!.UpdateState(aggregateRoot);
        
        List<IDomainEvent> domainEvents = aggregateRoot.DomainEvents.ToList();
        
        await _context.SaveChangesAsync(cancellationToken);
        aggregateRoot.ClearDomainEvents();

        return domainEvents;
    }
}