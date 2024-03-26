namespace Freethings.Shared.Abstractions.Persistence;

public record DbContextOptions
{
    public bool UseInMemoryProvider { get; init; } = false;
    public string ConnectionString { get; init; }
    public string MigrationHistoryTable { get; init; } = "__EFMigrationHistory";
    public string Schema { get; init; } = "dbo";
}