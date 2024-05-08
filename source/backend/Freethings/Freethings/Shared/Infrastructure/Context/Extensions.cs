using Freethings.Shared.Infrastructure.Context;

namespace Freethings.Shared.Infrastructure.Auth.Context;

public static class Extensions
{
    public static IServiceCollection AddCurrentUserContext(this IServiceCollection services)
    {
        services.AddSingleton<CurrentUserContextAccessor>();
        services.AddTransient(sp => sp.GetRequiredService<CurrentUserContextAccessor>().CurrentUser);
        return services;
    }
    
    public static IApplicationBuilder UseCurrentUserContext(this IApplicationBuilder app)
    {
        app.Use((httpContext, next) =>
        {
            CurrentUserContextAccessor currentUserContextAccessor = httpContext
                .RequestServices.GetRequiredService<CurrentUserContextAccessor>();
            
            currentUserContextAccessor.CurrentUser = new CurrentUser(httpContext);

            return next();
        });

        return app;
    }
}