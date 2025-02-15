namespace Discount.API
{
    public static class ServiceRegister
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            var assemblyMarker = typeof(Program).Assembly;
            var connectionString = builder.Configuration.GetConnectionString("Database");

            builder.Services.AddGrpc();
            builder.Services.AddDbContext<DiscountContext>(opts =>
            {
                opts.UseSqlite(connectionString);
            });

            return builder;
        }
    }
}
