using Freethings.Shared.Abstractions.Persistence;

namespace Freethings.Users.Infrastructure.Persistence;

public sealed record UserContextOptions : DbContextOptions
{
    public static string ModuleName => "Users";
}