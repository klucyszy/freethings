using System.Reflection;
using Freethings.Auctions.Domain;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Auctions.Infrastructure.Persistence;

public sealed class AuctionsContext : DbContext
{
    public AuctionsContext(DbContextOptions<AuctionsContext> options) : base(options)
    {
    }

    public DbSet<Auction> Auctions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}