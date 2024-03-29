namespace Freethings.Shared.Abstractions.Domain;

public abstract class AggregateRoot
{
    public Guid Id { get; set; }
    
    protected List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents
        => _domainEvents.AsReadOnly();

    protected AggregateRoot(Guid id)
    {
        Id = id;
    }
    
    protected AggregateRoot() { }

    protected void AddDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
    
}