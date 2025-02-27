﻿namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandHandler(
        IBasketRepository repository,
        IStoreBasketDiscountService discountService)
        : 
        ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await discountService.DeductDiscountAsync(command.Cart.Items, cancellationToken);

            var basket = await repository.StoreBasketAsync(command.Cart);

            return new StoreBasketResult(basket.UserName);
        }
    }
}
