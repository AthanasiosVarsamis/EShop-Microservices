namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/product/{productId}",

                async(Guid productId, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductByIdQuery(productId));

                    var response = result.Adapt<GetProductByIdResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Gets Product by Id")
            .WithDescription("Gets Product by Id");
        }
    }
}
