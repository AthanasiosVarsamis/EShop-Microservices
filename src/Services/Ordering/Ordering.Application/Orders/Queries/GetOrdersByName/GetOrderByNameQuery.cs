using FluentValidation;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public record GetOrderByNameQuery(string name)
    : IQuery<GetOrderByNameResult>;

public record GetOrderByNameResult(IEnumerable<OrderDto> Order);

public class GetOrderByNameQueryValidator : AbstractValidator<GetOrderByNameQuery>
{
    public GetOrderByNameQueryValidator()
    {
        RuleFor(x => x.name).NotEmpty().WithMessage("Name is Required"); 
    }
}
