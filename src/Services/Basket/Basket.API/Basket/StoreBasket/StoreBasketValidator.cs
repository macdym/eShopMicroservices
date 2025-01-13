namespace Basket.API.Basket.StoreBasket
{
    public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null.");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }
}
