using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrderByNameQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
    {
        public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value == query.name)
                .OrderBy(o => o.OrderName)
                .ToListAsync(cancellationToken);
            if (orders == null)
            {
                throw new NotFoundException($"Order with name {query.name} not found");
            }
            var ordersDtos=orders.ToOrderDtoList();

            return new GetOrderByNameResult(ordersDtos);
        }
    }
}
