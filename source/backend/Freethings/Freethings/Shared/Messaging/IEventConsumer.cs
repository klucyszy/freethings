using MediatR;

namespace Freethings.Shared.Messaging;

public interface IEventConsumer<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent;