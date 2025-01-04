
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,
                                       string Name,
                                       List<string> Category,
                                       string Description,
                                       string ImageFile,
                                       decimal Price) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess, string Message = default!);

    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session
                .LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
            {
                logger.LogError("UpdateProductCommandHandler.Handle Product for command: {@Command} not found.", command);
                return new UpdateProductResult(false, "Product not found.");
            }
            else
            {
                product.Name = command.Name;
                product.Category = command.Category;
                product.Description = command.Description;
                product.ImageFile = command.ImageFile;
                product.Price = command.Price;

                session.Update<Product>();

                await session.SaveChangesAsync(cancellationToken);
            }

            return new UpdateProductResult(true);
        }
    }
}
