namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var basket = (await session
                .LoadAsync<ShoppingCart>(userName, cancellationToken))
                ??
                throw new NotFoundException(nameof(ShoppingCart), userName);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            try
            {
                var basketDb = await GetBasketAsync(cart.UserName, cancellationToken);

                cart.Adapt(basketDb);

                session.Update(basketDb);
            }
            catch (NotFoundException)
            {
                session.Store(cart);
            }

            await session.SaveChangesAsync(cancellationToken);

            return cart;
        }

        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await GetBasketAsync(userName, cancellationToken);

            session.Delete(basket);

            await session.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
