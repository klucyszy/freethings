using Freethings.Auctions.Domain;
using Freethings.Auctions.Infrastructure.Persistence;
using Freethings.PublicApi.Events.Users;
using Freethings.Shared.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Auctions.Presentation.Events.External.UserCreated;

internal sealed class CreateAuctionUser : IEventConsumer<UserEvent.UserCreated>
{
    private readonly AuctionsContext _context;

    public CreateAuctionUser(AuctionsContext context)
    {
        _context = context;
    }
    
    // TODO: What if the handler fail due to e.g. DB connection error?
    public async Task Handle(UserEvent.UserCreated notification, CancellationToken cancellationToken)
    {
        if (await _context.AuctionUsers.AnyAsync(x => x.AppUserId == notification.UserId, cancellationToken))
        {
            return;
        }
        
        _context.AuctionUsers.Add(new AuctionUser(notification.UserId));

        await _context.SaveChangesAsync(cancellationToken);
    }
}