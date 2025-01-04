namespace Catalog.API.Products.GetProduct
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product? Product);

    public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);

            var product = await session
                .LoadAsync<Product>(query.Id, cancellationToken);

            if (product is null)
            {
                logger.LogError("GetProductsQueryHandler.Handle Product for query: {@Query} not found.", query);
            }

            return new GetProductByIdResult(product);
        }
    }
}
