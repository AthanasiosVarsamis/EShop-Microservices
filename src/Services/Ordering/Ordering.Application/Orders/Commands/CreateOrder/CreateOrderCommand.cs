using FluentValidation;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order)
 : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is Required");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is Required");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("There should be at least 1 order item");
    }
}