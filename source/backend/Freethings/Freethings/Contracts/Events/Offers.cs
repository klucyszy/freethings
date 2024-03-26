using Freethings.Shared.Abstractions.Domain;

namespace Freethings.Contracts.Events;

public abstract record OfferEvent : IDomainEvent
{
    public sealed record Published : OfferEvent;
}