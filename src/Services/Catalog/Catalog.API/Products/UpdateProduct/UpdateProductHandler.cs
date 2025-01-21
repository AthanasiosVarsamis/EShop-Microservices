using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid ProductId,string? Name, List<string>? Category, string? Description, string? ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductCommandValivator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValivator()
        {
            RuleFor(command => command.ProductId).NotEmpty().WithMessage("ProductId is Required");

            RuleFor(command => command.Name).NotEmpty()
                .WithMessage("Name is Required")
                .Length(2,150).WithMessage("Name must be between 2 and 150 characters");
        }
    }
    internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.ProductId, cancellationToken);

            if (product == null) {
                throw new ProductNotFoundException(command.ProductId);
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
