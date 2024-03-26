using Freethings.Shared.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using DbContextOptions = Freethings.Shared.Abstractions.Persistence.DbContextOptions;

namespace Freethings.Shared.Infrastructure.Persistence;

public static class DbContextExtensions
{
    public static IServiceCollection AddSqlServerDbContext<TDbContext, TDbContextOptions>(
        this IServiceCollection services,
        IConfiguration configuration,
        string moduleName = null)
        where TDbContext : DbContext
        where TDbContextOptions : DbContextOptions, new()
    {
        services.AddDbContext<TDbContext>(opts =>
        {
            TDbContextOptions dbContextOptions = configuration.GetOptions<TDbContextOptions>($"{moduleName}:SqlServer");

            if (dbContextOptions.UseInMemoryProvider)
            {
                opts.UseInMemoryDatabase(moduleName ?? nameof(TDbContext));
            }
            else
            {
                opts.UseSqlServer(
                    dbContextOptions.ConnectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsHistoryTable(
                            dbContextOptions.MigrationHistoryTable,
                            dbContextOptions.Schema);
                    });
            }
        });

        return services;
    }
}