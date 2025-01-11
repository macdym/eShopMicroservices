using Marten.Pagination;

namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string Category, int? PageNumber = 1, int? PageSize = 10) 
        : IQuery<GetProductsByCategoryResult>;

    public record GetProductsByCategoryResult(IEnumerable<Product> Product, long TotalItemCount = 0, long PageCount = 0);

    internal class GetProductsByCategoryQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session
                .Query<Product>()
                .Where(x => x.Category.Contains(query.Category))
                .ToPagedListAsync(query.PageNumber ?? 1,
                                  query.PageSize ?? 10,
                                  cancellationToken);

            return new GetProductsByCategoryResult(products);
        }
    }
}
