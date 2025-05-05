namespace Ordering.Application.Orders.EventHandlers;

internal class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handler:{DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
