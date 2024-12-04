namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
     : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValivator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValivator()
        {
            RuleFor(x =>x.Name).NotEmpty().WithMessage("Name is Required");
            RuleFor(x =>x.Category).NotEmpty().WithMessage("Category is Required");
            RuleFor(x =>x.Description).NotEmpty().WithMessage("Description is Required");
            RuleFor(x =>x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");
            RuleFor(x =>x.Price).NotEmpty().WithMessage("Price is Required");
        }

    }
    internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);

            //1. create Product Entity from command object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //2. save to database 
            session.Store(product); 
            await session.SaveChangesAsync();

            //3. return the createProductResult result
            return new CreateProductResult(product.Id);
        }
    }
}
