using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryRequest(string Category, int? PageNumber = 1, int? PageSize = 10);

    public record GetProductsByCategoryResponse(IEnumerable<Product> Products, long TotalItemCount = 0, long PageCount = 0);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/products/category/{category}",
                async ([AsParameters] GetProductsByCategoryRequest request, ISender sender) =>
                {
                    var query = request.Adapt<GetProductsByCategoryQuery>();

                    var result = await sender.Send(query);

                    var response = result.Adapt<GetProductsByCategoryResponse>();

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
