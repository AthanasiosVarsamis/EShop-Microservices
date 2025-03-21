namespace Ordering.Domain.Events;

public record class OrderUpdatedEvent(Order order) : IDomainEvent;
