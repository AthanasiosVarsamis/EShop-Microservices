namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid ProductId,string? Name, List<string>? Category, string? Description, string? ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);

            var product = await session.LoadAsync<Product>(command.ProductId, cancellationToken);

            if (product == null) {
                throw new ProductNotFoundException();
            }

            product.Name = command.Name ?? product.Name;
            product.Category = command.Category ??  product.Category;
            product.Description = command.Description ??  product.Description;
            product.ImageFile = command.ImageFile ?? product.ImageFile;
            product.Price = command.Price != 0 ? command.Price : product.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);

        }
    }
}
