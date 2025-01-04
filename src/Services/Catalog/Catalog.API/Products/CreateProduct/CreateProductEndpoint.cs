namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(CreateProductDto Dto);

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(
                "/products",
                async (CreateProductRequest request, ISender sender) =>
                {
                    var result = await sender.Send(new CreateProductCommand(request.Dto));

                    var response = result.Adapt<CreateProductResponse>();

                    return Results.Created($"/products/{response.Id}", response);
                })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Create Product")
                .WithSummary("Create Prodct");
        }
    }
}
