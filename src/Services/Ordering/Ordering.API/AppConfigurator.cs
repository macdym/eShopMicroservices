namespace Ordering.API
{
    public static class AppConfigurator
    {
        public static WebApplication ConfigureApp(this WebApplication app)
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
