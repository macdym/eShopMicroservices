using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public class StoreBasketDiscountService(
        DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        :
        IStoreBasketDiscountService
    {
        public async Task DeductDiscountAsync(List<ShoppingCartItem> items, CancellationToken cancellationToken)
        {
            foreach (var item in items)
            {
                await SetDiscountAsync(item,  cancellationToken);
            }
        }

        private async Task SetDiscountAsync(ShoppingCartItem item, CancellationToken cancellationToken)
        {
            var request = new GetDiscountReqest()
            {
                ProductName = item.ProductName
            };

            var couponModel = await discountProtoServiceClient.GetDiscountAsync(request,
                                                                                cancellationToken: cancellationToken);
            item.Price -= GetDiscount(couponModel, item.Price);
        }

        private decimal GetDiscount(CouponModel couponModel, decimal price)
        {
            var result = couponModel?.DiscountType switch
            {
                nameof(DiscountType.Percentage) => GetPercentageDiscount(couponModel.Amount, price),
                nameof(DiscountType.Cash) => couponModel.Amount,
                _ => 0
            };

            return result;
        }

        private decimal GetPercentageDiscount(int amount, decimal price)
        {
            return price * amount / 100;
        }
    }
}
