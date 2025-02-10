namespace Discount.API
{
    public static class ServiceRegister
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            var assemblyMarker = typeof(Program).Assembly;
            var connectionString = builder.Configuration.GetConnectionString(ServiceRegisterConst.DATABASE);

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            return builder;
        }
    }
}
