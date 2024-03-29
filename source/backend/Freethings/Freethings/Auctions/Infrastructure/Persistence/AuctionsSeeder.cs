using Freethings.Auctions.Domain;
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
            _context.Auctions.Add(new AuctionAdvert(
                    Quantity.Create(10),
                    AuctionType.Manual,
                    Title.Create("Sample item"),
                    Description.Create("Description"))
                {
                    Id = ManualAuctionId
                }
            );
        }
        
        if (!_context.Auctions.Any(p => p.Id == AutoAuctionId))
        {
            _context.Auctions.Add(new AuctionAdvert(
                    Quantity.Create(10),
                    AuctionType.FirstComeFirstServed,
                    Title.Create("Sample item"),
                    Description.Create("Description"))
                {
                    Id = AutoAuctionId
                }
            );
        }
        
        _context.SaveChanges();
    }
}