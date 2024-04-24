using System.Security.Claims;
using Freethings.Shared.Abstractions.Auth.Context;

namespace Freethings.Shared.Infrastructure.Auth.Context;

internal sealed class CurrentUser : ICurrentUser
{
    public Guid? Identity { get; }
    public bool IsAuthenticated { get; }

    public CurrentUser(HttpContext httpContext)
    {
        ClaimsPrincipal user = httpContext.User;
        
        IsAuthenticated = user.Identity?.IsAuthenticated ?? false;
        Identity = IsAuthenticated
            ? Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!)
            : null;
    }
}