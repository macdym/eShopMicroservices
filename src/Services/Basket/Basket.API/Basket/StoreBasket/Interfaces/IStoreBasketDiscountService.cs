namespace Basket.API.Basket.StoreBasket.Interfaces
{
    public interface IStoreBasketDiscountService
    {
        Task DeductDiscountAsync(List<ShoppingCartItem> items, CancellationToken cancellationToken);
    }
}
