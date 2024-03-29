using Freethings.Shared.Abstractions.Domain;

namespace Freethings.Shared.Abstractions.Messaging;

public interface IEventConsumer<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent;