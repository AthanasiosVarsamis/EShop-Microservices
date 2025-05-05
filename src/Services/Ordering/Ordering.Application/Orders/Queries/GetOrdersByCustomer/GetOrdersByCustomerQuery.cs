using FluentValidation;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(Guid customerId)
    : IQuery<GetOrdersByCustomerResult>;

public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomerQueryValidator : AbstractValidator<GetOrdersByCustomerQuery>
{
    public GetOrdersByCustomerQueryValidator()
    {
        RuleFor(x => x.customerId).NotEmpty().WithMessage("customerId is Required");
    }
}

