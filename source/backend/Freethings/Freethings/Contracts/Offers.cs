using Freethings.Shared.Messaging;

namespace Freethings.Contracts;

public abstract record OfferEvent : IEvent
{
    public sealed record Published : OfferEvent;
}