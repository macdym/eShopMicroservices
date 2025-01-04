using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    
    public record DeleteProductResult(bool IsSuccess, string Message = default!);

    internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) 
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);

            var product = await session
                .LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
            {
                logger.LogError("DeleteProductCommandHandler.Handle Product for Command: {@Command} not found.", command);
                return new DeleteProductResult(false, "Product not found.");
            }

            session.Delete<Product>(command.Id);
            
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
