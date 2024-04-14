using Freethings.Shared.Infrastructure.Persistence;
using Freethings.Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Users.Infrastructure.Persistence;

internal sealed class UsersContext : DbContext
{
    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromNamespace(typeof(User).Namespace);
    }
}