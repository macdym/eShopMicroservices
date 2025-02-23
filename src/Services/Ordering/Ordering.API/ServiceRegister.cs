namespace Ordering.API
{
    public static class ServiceRegister
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddCarter();

            return builder;
        }
    }
}
