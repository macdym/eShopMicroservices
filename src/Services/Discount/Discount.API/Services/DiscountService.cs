namespace Discount.API.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountReqest request, ServerCallContext context)
        {
            var coupon = (await dbContext
                .Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName))
                ??
                new Coupon
                {
                    ProductName = "No discount",
                    Description = "",
                    Amount = 0
                };

            logger.LogInformation(
                "Discount is retrieved for ProductName: {ProductName}, Amount: {Amount}",
                coupon.ProductName, coupon.Amount);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> CrateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Adapt<Coupon>();

            dbContext.Coupons.Add(coupon);

            await dbContext.SaveChangesAsync();

            logger.LogInformation(
                "Discount is created for ProductName: {ProductName}, Description: {Descritpion}, Amount: {Amount}",
                coupon.ProductName, coupon.Description, coupon.Amount);

            return coupon.Adapt<CouponModel>();
        }

        public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            return base.UpdateDiscount(request, context);
        }

        public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return base.DeleteDiscount(request, context);
        }
    }
}
