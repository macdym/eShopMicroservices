using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API
{
    public static class AppConfigurator
    {
        public static WebApplication Configure(this WebApplication app)
        {
            app.MapCarter();

            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    if(exception is null)
                    {
                        return;
                    }

                    var problemDetails = new ProblemDetails
                    {
                        Title = exception.Message,
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = exception.StackTrace
                    };

                    context.RequestServices
                        .GetRequiredService<ILogger<Program>>()
                        .LogError(exception, exception.Message);

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/problem+json";

                    await context.Response.WriteAsJsonAsync(problemDetails);
                });
            });

            return app;
        }
    }
}
