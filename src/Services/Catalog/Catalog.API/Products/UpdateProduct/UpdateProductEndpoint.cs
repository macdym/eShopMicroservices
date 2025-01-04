﻿
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id,
                                       string Name,
                                       List<string> Category,
                                       string Description,
                                       string ImageFile,
                                       decimal Price);
    public record UpdateProductResponse(bool IsSuccess, string Message = default!);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut(
                "/products",
                async (UpdateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateProductCommand>();

                    var result = await sender.Send(command);

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
