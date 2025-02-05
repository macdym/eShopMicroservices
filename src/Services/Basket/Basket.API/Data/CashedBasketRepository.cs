
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CashedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) 
        : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }

            var basket = await basketRepository.GetBasketAsync(userName, cancellationToken);

            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            var basketDb = await basketRepository.StoreBasketAsync(basket, cancellationToken);

            await cache.SetStringAsync(basketDb.UserName, JsonSerializer.Serialize(basketDb), cancellationToken);

            return basketDb;
        }

        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            if (await basketRepository.DeleteBasketAsync(userName, cancellationToken))
            {
                await cache.RemoveAsync(userName, cancellationToken);
            }
            
            return true;
        }
    }
}
