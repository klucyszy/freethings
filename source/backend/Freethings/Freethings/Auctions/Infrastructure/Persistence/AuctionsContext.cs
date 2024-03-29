using Freethings.Auctions.Domain;
using Freethings.Auctions.Infrastructure.Persistence.Configurations;
using Freethings.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Auctions.Infrastructure.Persistence;

internal sealed class AuctionsContext : DbContext
{
    public AuctionsContext(DbContextOptions<AuctionsContext> options) : base(options)
    {
    }

    public DbSet<AuctionAdvert> Auctions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromNamespace(typeof(AuctionEntityConfiguration).Namespace);
    }
}