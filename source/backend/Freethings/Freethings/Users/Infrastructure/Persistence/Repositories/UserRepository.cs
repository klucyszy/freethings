using Freethings.Users.Domain;
using Freethings.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Users.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly UsersContext _context;

    public UserRepository(UsersContext context)
    {
        _context = context;
    }

    public async Task<User> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context
            .Users
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string identityProviderIdentifier, CancellationToken cancellationToken)
    {
        return await _context
            .Users
            .AnyAsync(p => p.IdentityProviderIdentifier == identityProviderIdentifier, cancellationToken);
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }
}