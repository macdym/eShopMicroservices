namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand;

    public class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand>
    {
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            session.DeleteWhere<Product>(x => x.Id == request.Id);
            
            await session.SaveChangesAsync(cancellationToken);

            return new Unit();
        }
    }
}
