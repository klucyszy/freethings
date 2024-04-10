namespace Freethings.Users.Application.Services;

public interface IIdentityProviderService
{
    Task SaveUserIdAsync(string identity, string userId, CancellationToken cancellationToken = default);
}