using System.Reflection;
using Freethings.Offers.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Offers.Infrastructure.Persistence;

public sealed class OffersContext : DbContext
{
    public OffersContext(DbContextOptions<OffersContext> options) : base(options)
    {
    }

    public DbSet<Offer> Offers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}