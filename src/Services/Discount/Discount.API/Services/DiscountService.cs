namespace Discount.API.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountReqest request, ServerCallContext context)
        {
            var coupon = (await GetCouponAsync(request.ProductName))
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
            var coupon = request.Coupon.Adapt<Coupon>();

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation(
                "Discount is created for ProductName: {ProductName}, Description: {Descritpion}, Amount: {Amount}",
                coupon.ProductName, coupon.Description, coupon.Amount);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = await GetCouponAsync(request.Coupon.ProductName);

            if(coupon is null)
            {
                var createRequset = request.Adapt<CreateDiscountRequest>();

                var createdCoupon = await CrateDiscount(createRequset, context);

                return createdCoupon.Adapt<CouponModel>();
            }

            coupon.Description = coupon.Description;
            coupon.Amount = coupon.Amount;
            await dbContext.SaveChangesAsync();

            logger.LogInformation(
                "Discount is updated for ProductName: {ProductName}, Description: {Descritpion}, Amount: {Amount}",
                coupon.ProductName, coupon.Description, coupon.Amount);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await GetCouponAsync(request.ProductName);

            if(coupon is not null)
            {
                dbContext.Coupons.Remove(coupon);
                await dbContext.SaveChangesAsync();
                
                logger.LogInformation(
                    "Discount is deleted for ProductName: {ProductName}", coupon.ProductName);
            }



            return new DeleteDiscountResponse { Success = true };
        }

        private async Task<Coupon?> GetCouponAsync(string productName)
        {
            return await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == productName);
        }
    }
}
