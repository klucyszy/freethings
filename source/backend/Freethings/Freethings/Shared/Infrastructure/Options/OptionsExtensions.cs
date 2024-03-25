namespace Freethings.Shared.Infrastructure.Options;

public static class OptionsExtensions
{
    public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName)
        where TOptions : class, new()
    {
        TOptions options = new();
        configuration.GetSection(sectionName).Bind(options);
        
        return options;
    }
}