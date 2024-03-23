using Freethings.Offers.Application.Entities;
using Freethings.Offers.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Offers.Infrastructure.Persistence.Repositories;

public sealed class OfferRepository : IOfferRepository
{
    private readonly OffersContext _context;

    public OfferRepository(OffersContext context)
    {
        _context = context;
    }

    public async Task<Offer> GetAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Offers.FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<Offer> AddAsync(Offer offer, CancellationToken ct = default)
    {
        _context.Add(offer);

        await _context.SaveChangesAsync(ct);

        return offer;
    }

    public async Task<Offer> UpdateAsync(Offer offer, CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);

        return offer;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        Offer offer = await _context.Offers.FindAsync(id, ct);
        
        if (offer is null)
        {
            return false;
        }
        
        _context.Offers.Remove(offer);
        
        await _context.SaveChangesAsync(ct);
        
        return true;
    }
}