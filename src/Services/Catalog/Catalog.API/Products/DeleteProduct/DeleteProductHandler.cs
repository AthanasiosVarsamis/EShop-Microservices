
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValivator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValivator()
        {
            RuleFor(command => command.ProductId).NotEmpty().WithMessage("ProductId is Required");
        }
    }
    internal class DeleteProducCommandtHandler(IDocumentSession session, ILogger<DeleteProducCommandtHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProducCommandtHandler.Handle called with {@Query}", command);

            try
            {
                //session.DeleteWhere<Product>(x => x.Id == command.ProductId);
                session.Delete<Product>(command.ProductId);
                await session.SaveChangesAsync(cancellationToken);
                
                return new DeleteProductResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }
    }
}
