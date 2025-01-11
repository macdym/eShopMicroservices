namespace Catalog.API
{
    public static class AppConfigurator
    {
        public static WebApplication Configure(this WebApplication app)
        {
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
