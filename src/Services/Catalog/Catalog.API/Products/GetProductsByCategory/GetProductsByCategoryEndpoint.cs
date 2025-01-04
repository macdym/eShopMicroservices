
using Catalog.API.Products.GetProduct;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategory(IEnumerable<Product> Products);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/products/category/{category}",
                async (string category, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductsByCategoryQuery(category));

                    var response = result.Adapt<GetProductsByCategory>();

                    return Results.Ok(result);
                })
                .WithName("GetProductsByCategory")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Get Products By Category")
                .WithSummary("Get Products By Category");
        }
    }
}
