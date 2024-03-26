using Freethings.Auctions.Domain;
using Freethings.Auctions.Infrastructure.Persistence.Entities;
using Freethings.Auctions.Infrastructure.Persistence.Entities.ValueObjects;
using Freethings.Shared.Abstractions.Persistence;

namespace Freethings.Auctions.Infrastructure.Persistence;

internal sealed class AuctionsSeeder : IDataSeeder
{
    private static Guid AuctionId => Guid.Parse("00000000-0000-0000-0000-000000000001");

    private readonly AuctionsContext _context;

    public AuctionsSeeder(AuctionsContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Auctions.Any(p => p.Id == AuctionId))
        {
            return;
        }

        _context.Auctions.Add(new AuctionEntity(
            10,
            Auction.AuctionType.Manual,
            new AuctionMetadataEntity(
                AuctionTitle.Create("Sample item"),
                AuctionDescription.Create("Sample description")
            ))
            {
                Id = AuctionId
            }
        );

        _context.SaveChanges();
    }
}