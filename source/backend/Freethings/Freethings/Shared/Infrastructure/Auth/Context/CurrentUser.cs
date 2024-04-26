using System.Security.Claims;
using Freethings.Shared.Abstractions.Auth.Context;

namespace Freethings.Shared.Infrastructure.Auth.Context;

internal sealed class CurrentUser : ICurrentUser
{
    private const string AppUserIdClaimType = "https://freethings/api/appUserId";
    private const string RolesClaimType = "https://freethings/api/roles";
    private const string AdminRole = "admin";
    
    public Guid? Identity { get; }
    public bool IsAuthenticated { get; }
    public bool IsAdmin { get; }

    public CurrentUser(HttpContext httpContext)
    {
        ClaimsPrincipal user = httpContext.User;
        
        IsAuthenticated = user.Identity?.IsAuthenticated ?? false;
        Identity = IsAuthenticated
            ? Guid.Parse(user.FindFirstValue(AppUserIdClaimType)!)
            : null;
        IsAdmin = user.FindAll(RolesClaimType)
            .Any(claim => claim.Value == AdminRole);
    }
}