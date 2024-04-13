namespace Freethings.Users.Domain.Repositories;

public interface IUserRepository
{
    public Task<User> GetAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> ExistsAsync(string identityProviderIdentifier, CancellationToken cancellationToken);
    public Task<User> AddAsync(User user, CancellationToken cancellationToken);
}