namespace Freethings.Users.Domain;

public sealed class User
{
    public Guid Id { get; private set; }
    public string IdentityProviderIdentifier { get; private set; }
    public string Username { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private User() {}

    public User(string identityProviderIdentifier, string username, DateTimeOffset createdAt)
    {
        IdentityProviderIdentifier = identityProviderIdentifier;
        Username = username;
        CreatedAt = createdAt;
    }
}