using Freethings.Auctions.Domain;
using Freethings.Shared.Abstractions.Persistence;

namespace Freethings.Auctions.Infrastructure.Persistence;

internal sealed class AuctionsSeeder : IDataSeeder
{
    private static Guid UserId => Guid.Parse("00000000-0000-0000-0000-000000000001");
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
            AuctionAdvert manualAdvert = new AuctionAdvert(
                UserId,
                Quantity.Create(10),
                AuctionType.Manual,
                Title.Create("Sample item"),
                Description.Create("Description"),
                DateTimeOffset.UtcNow)
            {
                Id = ManualAuctionId
            };
            
            // manualAdvert.Publish(DateTimeOffset.UtcNow);

            _context.Auctions.Add(manualAdvert);
        }

        if (!_context.Auctions.Any(p => p.Id == AutoAuctionId))
        {
            AuctionAdvert autoAdvert = new AuctionAdvert(
                UserId,
                Quantity.Create(10),
                AuctionType.FirstComeFirstServed,
                Title.Create("Sample item"),
                Description.Create("Description"),
                DateTimeOffset.UtcNow)
            {
                Id = AutoAuctionId
            };
            
            autoAdvert.Publish(DateTimeOffset.UtcNow);
            
            _context.Auctions.Add(autoAdvert);
        }

        _context.SaveChanges();
    }
}