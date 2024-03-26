using System.Reflection;
using Freethings.Shared.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Shared.Infrastructure.Persistence;

public static class MigrationsExtensions
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;
        
        IEnumerable<Type> dbContextTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(DbContext)) && !t.IsAbstract);

        foreach (Type dbContextType in dbContextTypes)
        {
            using DbContext context = (DbContext) serviceProvider.GetRequiredService(dbContextType);
            
            context.Database.Migrate();
        }

        return app;
    }
    
    public static WebApplication SeedWithSampleData(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;

        IEnumerable<IDataSeeder> dataSeeders = serviceProvider.GetServices<IDataSeeder>();
        
        foreach (IDataSeeder seeder in dataSeeders)
        {
            seeder.Seed();
        }

        return app;
    }
}