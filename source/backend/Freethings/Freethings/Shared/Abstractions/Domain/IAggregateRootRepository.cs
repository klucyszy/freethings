namespace Freethings.Shared.Abstractions.Domain;

public interface IAggregateRootRepository<TAggregate>
    where TAggregate : AggregateRoot
{
    Task<TAggregate> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveAsync(TAggregate aggregateRoot, CancellationToken cancellationToken = default);
}