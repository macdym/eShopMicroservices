namespace Catalog.API.Products.GetProduct
{
    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/products/{id}",
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductByIdQuery(id));

                    if (result.Product is null)
                    {
                        return Results.NotFound(new { Message = "Product not found." });
                    }

                    var response = result.Adapt<GetProductByIdResponse>();

                    return Results.Ok(result);
                })
                .WithName("GetProductById")
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithDescription("Get Product By Id")
                .WithSummary("Get Product By Id");
        }
    }
}
