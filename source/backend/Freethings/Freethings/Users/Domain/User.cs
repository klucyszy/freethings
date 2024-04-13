namespace Freethings.Users.Domain;

public sealed class User
{
    public Guid Id { get; private set; }
    public string IdentityProviderIdentifier { get; private set; }
    public string Username { get; private set; }

    private User() {}

    public User(string identityProviderIdentifier, string username)
    {
        IdentityProviderIdentifier = identityProviderIdentifier;
        username = Username;
    }
}