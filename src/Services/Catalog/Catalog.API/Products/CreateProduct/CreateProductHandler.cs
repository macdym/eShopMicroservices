namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(CreateProductDto Dto) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Dto.Name,
                Category = command.Dto.Category,
                Description = command.Dto.Description,
                ImageFile = command.Dto.ImageFile,  
                Price = command.Dto.Price
            };

            session.Store(product);

            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
