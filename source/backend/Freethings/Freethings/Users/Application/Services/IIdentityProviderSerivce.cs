namespace Freethings.Users.Application.Services;

public interface IIdentityProviderSerivce
{
    Task SaveUserIdAsync(string identity, string userId, CancellationToken cancellationToken = default);
}