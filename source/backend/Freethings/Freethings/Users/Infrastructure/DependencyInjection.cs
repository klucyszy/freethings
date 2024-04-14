using Freethings.Shared.Infrastructure.Persistence;
using Freethings.Users.Domain.Repositories;
using Freethings.Users.Infrastructure.Auth0;
using Freethings.Users.Infrastructure.Persistence;
using Freethings.Users.Infrastructure.Persistence.Repositories;

namespace Freethings.Users.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSqlServerDbContext<UsersContext, UserContextOptions>(configuration,
            UserContextOptions.ModuleName);

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddAuth0ManagementApi(configuration);
        
        return services;
    }
}