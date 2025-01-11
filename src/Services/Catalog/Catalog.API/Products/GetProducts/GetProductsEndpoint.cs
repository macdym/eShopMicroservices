﻿namespace Catalog.API.Products.GetProducts
{
    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetProductsResponse(IEnumerable<Product> Products, long TotalItemCount = 0, long PageCount = 0);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/products",
                async ([AsParameters] GetProductsRequest request,ISender sender) =>
                {
                    var query = request.Adapt<GetProductsQuery>();

                    var result = await sender.Send(query);

                    var response = result.Adapt<GetProductsResponse>();

                    return Results.Ok(response);
                })
                .WithName("GetProducts")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Get Products")
                .WithSummary("Get Products");
        }
    }
}
