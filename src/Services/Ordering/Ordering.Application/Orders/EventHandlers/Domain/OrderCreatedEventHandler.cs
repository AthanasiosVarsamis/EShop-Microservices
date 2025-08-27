using MassTransit;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.EventHandlers.Domain;
//we need publishEndpoint in order to create Integration Event
internal class OrderCreatedEventHandler
    (IPublishEndpoint publishEndpoint ,ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handler:{DomainEvent}", domainEvent.GetType().Name);

        var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();

        await publishEndpoint.Publish(orderCreatedIntegrationEvent,cancellationToken);
    }
}
