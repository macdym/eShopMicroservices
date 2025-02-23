namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.AddApplicationServices();
            builder.AddInfrastructureServices();
            builder.AddApiServices();

            return builder;
        }

        private static WebApplicationBuilder AddApiServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddCarter();

            return builder;
        }

        public static WebApplication Configure(this WebApplication app)
        {
            app.UseHttpsRedirection();

            app.MapCarter();

            app.UseExceptionHandler(opts => { });

            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            return app;
        }
    }
}
