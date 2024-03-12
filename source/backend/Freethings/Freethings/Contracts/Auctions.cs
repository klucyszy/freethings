using Freethings.Shared.Messaging;

namespace Freethings.Contracts;

public abstract record AuctionEvent : IEvent
{
    public sealed record ItemClaimed : AuctionEvent;
}