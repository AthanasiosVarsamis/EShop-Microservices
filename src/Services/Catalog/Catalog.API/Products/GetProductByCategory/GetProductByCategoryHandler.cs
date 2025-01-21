
namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category): IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryHandler(IDocumentSession session) :
        IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            //var product = await session.LoadManyAsync<Product>(query.Category, cancellationToken);
            var products = await session.Query<Product>()
                                .Where(x => x.Category.Contains(query.Category))
                                .ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);                                
        }
    }
}
