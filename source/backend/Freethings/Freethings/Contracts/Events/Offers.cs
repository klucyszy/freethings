using Freethings.Shared.Messaging;

namespace Freethings.Contracts.Events;

public abstract record OfferEvent : IEvent
{
    public sealed record Published : OfferEvent;
}