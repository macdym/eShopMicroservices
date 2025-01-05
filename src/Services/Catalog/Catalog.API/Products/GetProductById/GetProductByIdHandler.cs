﻿using BuildingBlocks.CustomExceptions;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product? Product);

    internal class GetProductByIdQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = (await session
                .LoadAsync<Product>(query.Id, cancellationToken))
                ??
                throw new NotFoundException(nameof(Product), query.Id);

            return new GetProductByIdResult(product);
        }
    }
}
