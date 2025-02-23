namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddInfrastructureServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Database");

            //builder.Services.AddDbContext
            return builder;
        }
    }
}
