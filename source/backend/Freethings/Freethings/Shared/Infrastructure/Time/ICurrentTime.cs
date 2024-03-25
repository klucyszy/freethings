namespace Freethings.Shared.Infrastructure.Time;

public interface ICurrentTime
{
    DateTimeOffset Now();
    DateTimeOffset UtcNow();
}

public sealed class CurrentTime : ICurrentTime
{
    public DateTimeOffset Now() => DateTimeOffset.Now;
    public DateTimeOffset UtcNow() => DateTimeOffset.UtcNow;
}

public static class CurrentTimExtensions
{
    public static IServiceCollection AddCurrentTime(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentTime, CurrentTime>();

        return services;
    }
}

