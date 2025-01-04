
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(UpdateProductDto ProductDto) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess, string Message = default!);

    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session
                .LoadAsync<Product>(command.ProductDto.Id, cancellationToken);

            if (product is null)
            {
                logger.LogError("UpdateProductCommandHandler.Handle Product for command: {@Command} not found.", command);
                return new UpdateProductResult(false, "Product not found.");
            }
            else
            {
                product.Name = command.ProductDto.Name;
                product.Category = command.ProductDto.Category;
                product.Description = command.ProductDto.Description;
                product.ImageFile = command.ProductDto.ImageFile;
                product.Price = command.ProductDto.Price;

                session.Update<Product>();

                await session.SaveChangesAsync(cancellationToken);
            }

            return new UpdateProductResult(true);
        }
    }
}
