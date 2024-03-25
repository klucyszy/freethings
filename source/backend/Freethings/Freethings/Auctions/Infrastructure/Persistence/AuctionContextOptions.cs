using Freethings.Shared.Infrastructure.Persistence;

namespace Freethings.Auctions.Infrastructure.Persistence;

public sealed record AuctionContextOptions : DbContextOptions
{
    public static string SectionName => "Auctions:SqlServer";
}