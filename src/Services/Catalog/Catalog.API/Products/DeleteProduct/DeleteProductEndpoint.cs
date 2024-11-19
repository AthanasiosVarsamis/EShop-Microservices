namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/product/delete/{ProductId}",
                async (Guid ProductId, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteProductCommand(ProductId));

                    var response = result.Adapt<DeleteProductResponse>();

                    return Results.Ok(response);
                })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product"); 
        }
    }
}
