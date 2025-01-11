namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart ShoppingCart);

    internal class GetBasketQueryHandler(IDocumentSession session) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var shoppingCart = (await session
                .Query<ShoppingCart>()
                .SingleOrDefaultAsync(x => x.UserName == query.UserName, cancellationToken))
                ??
                throw new NotFoundException(nameof(ShoppingCart), query.UserName);

            return new GetBasketResult(shoppingCart);
        }
    }
}
