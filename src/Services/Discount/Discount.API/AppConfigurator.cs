﻿namespace Discount.API
{
    public static class AppConfigurator
    {
        public static WebApplication Configure(this WebApplication app)
        {
            app.UseMigration();
            app.MapGrpcService<DiscountService>();
            app.MapGet("/", () => "Discount service");
            app.UseExceptionHandler(opts => { });

            return app;
        }
    }
}
