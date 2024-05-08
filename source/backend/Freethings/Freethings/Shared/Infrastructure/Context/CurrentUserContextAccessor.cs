using Freethings.Shared.Abstractions.Auth.Context;

namespace Freethings.Shared.Infrastructure.Context;

internal sealed class CurrentUserContextAccessor
{
    private static readonly AsyncLocal<CurrentUserContextHolder> _holder = new();
    
    public ICurrentUser CurrentUser
    {
        get => _holder.Value?.Context;
        set
        {
            CurrentUserContextHolder currentUserContextHolder = _holder.Value;
            
            if (currentUserContextHolder != null) currentUserContextHolder.Context = null;
            if (value == null)  return;
            
            _holder.Value = new CurrentUserContextHolder
            {
                Context = value
            };
        }
    }
    
    private sealed class CurrentUserContextHolder
    {
        public ICurrentUser Context;
    }
}