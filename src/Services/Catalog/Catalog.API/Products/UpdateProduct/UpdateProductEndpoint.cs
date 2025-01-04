
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(UpdateProductDto Dto);
    public record UpdateProductResponse(bool IsSuccess, string Message = default!);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut(
                "/products",
                async (UpdateProductRequest request, ISender sender) =>
                {
                    var result = await sender.Send(new UpdateProductCommand(request.Dto));

                    if (!result.IsSuccess)
                    {
                        return Results.NotFound(result.Message);
                    }

                    var response = result.Adapt<UpdateProductResponse>();

                    return Results.Ok(response);
                })
                .WithName("UpdateProduct")
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithDescription("Update Product")
                .WithSummary("Update Product");
        }
    }
}
