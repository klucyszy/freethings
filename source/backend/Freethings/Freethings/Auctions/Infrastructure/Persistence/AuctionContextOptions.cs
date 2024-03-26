using Freethings.Shared.Abstractions.Persistence;

namespace Freethings.Auctions.Infrastructure.Persistence;

public sealed record AuctionContextOptions : DbContextOptions
{
    public static string ModuleName => "Auctions";
}