using Freethings.Auctions.Domain;
using Freethings.Auctions.Infrastructure.Persistence.Configurations;
using Freethings.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Users.Infrastructure.Persistence;

internal sealed class UsersContext : DbContext
{
    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromNamespace(typeof(AuctionAdvertConfiguration).Namespace);
    }
}