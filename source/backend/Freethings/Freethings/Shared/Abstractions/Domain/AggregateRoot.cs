using Freethings.Shared.Abstractions.Messaging;

namespace Freethings.Shared.Abstractions.Domain;

public abstract class AggregateRoot
{
    public Guid Id { get; }
    
    private readonly List<IEvent> _domainEvents = [];

    public IReadOnlyCollection<IEvent> DomainEvents
        => _domainEvents.AsReadOnly();

    protected AggregateRoot(Guid id)
    {
        Id = id;
    }
    
    protected AggregateRoot() { }

    protected void AddDomainEvent(IEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearEvents()
        => _domainEvents.Clear();
    
}