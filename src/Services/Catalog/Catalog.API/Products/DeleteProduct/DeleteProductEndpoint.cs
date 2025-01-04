
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid Id);

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            //app.MapDelete(
            //    "/products/{Id}", 
            //    async (DeleteProductRequest request, ISender sender) =>
            //    {
            //        var productId = request.Adapt<DeleteProductCommand>();

            //        await sender.Send(productId);

            //        return Results.NoContent();
            //    })
            //    .WithName("DeleteProduct")
            //    .Produces(StatusCodes.Status204NoContent)
            //    .ProducesProblem(StatusCodes.Status400BadRequest)
            //    .WithDescription("Delete Product")
            //    .WithSummary("Delete Prodct");
        }
    }
}
