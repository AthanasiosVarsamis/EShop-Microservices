
using JasperFx.Core;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId): IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) :
        IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query);

            var product = await  session.LoadAsync<Product>(query.ProductId,cancellationToken);
            //var product = await  session.Query<Product>()
            //                            .Where(x => x.Id == query.ProductId)
            //                            .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            return new GetProductByIdResult(product);
        }
    }
}
