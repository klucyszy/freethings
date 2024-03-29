using Freethings.Auctions.Domain;
using Freethings.Auctions.Infrastructure.Persistence.Entities;
using Freethings.Auctions.Infrastructure.Persistence.Entities.ValueObjects;
using Freethings.Shared.Abstractions.Persistence;

namespace Freethings.Auctions.Infrastructure.Persistence;

internal sealed class AuctionsSeeder : IDataSeeder
{
    private static Guid ManualAuctionId => Guid.Parse("00000000-0000-0000-0000-000000000001");
    private static Guid AutoAuctionId => Guid.Parse("00000000-0000-0000-0000-000000000002");

    private readonly AuctionsContext _context;

    public AuctionsSeeder(AuctionsContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (!_context.Auctions.Any(p => p.Id == ManualAuctionId))
        {
            _context.Auctions.Add(new AuctionEntity(
                    10,
                    AuctionType.Manual,
                    new AuctionMetadataEntity(
                        AuctionTitle.Create("Sample item"),
                        AuctionDescription.Create("Sample description")
                    ))
                {
                    Id = ManualAuctionId
                }
            );
        }
        
        if (!_context.Auctions.Any(p => p.Id == AutoAuctionId))
        {
            _context.Auctions.Add(new AuctionEntity(
                    10,
                    AuctionType.FirstComeFirstServed,
                    new AuctionMetadataEntity(
                        AuctionTitle.Create("Sample item 2"),
                        AuctionDescription.Create("Sample description 2")
                    ))
                {
                    Id = AutoAuctionId
                }
            );
        }
        
        _context.SaveChanges();
    }
}