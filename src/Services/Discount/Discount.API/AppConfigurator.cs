namespace Discount.API
{
    public static class AppConfigurator
    {
        public static WebApplication ConfigureApp(this WebApplication app)
        {
            app.UseExceptionHandler(opts => { });

            return app;
        }
    }
}
