using MediatR;

namespace Freethings.Shared.Abstractions.Messaging;

public interface IEventConsumer<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent;