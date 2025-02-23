namespace Discount.API
{
    public static class ServiceRegister
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddGrpc();
            builder.Services.AddDbContext<DiscountContext>(opts =>
            {
                opts.UseSqlite(builder.Configuration.GetConnectionString("Database"));
            });

            return builder;
        }
    }
}
