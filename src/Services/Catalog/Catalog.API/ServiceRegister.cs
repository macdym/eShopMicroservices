using BuildingBlocks.Behaviors;
using BuildingBlocks.CustomExceptions.Handlers;
using Catalog.API.Data;

namespace Catalog.API
{
    public static class ServiceRegister
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            var assemblyMarker = typeof(Program).Assembly;

            builder.Services.AddCarter();

            builder.Services.AddMediatR((config) =>
            {
                config.RegisterServicesFromAssembly(assemblyMarker);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            builder.Services.AddValidatorsFromAssembly(assemblyMarker);

            builder.Services.AddMarten(opts =>
            {
                opts.Connection(builder.Configuration.GetConnectionString("Database")!);
            }).UseLightweightSessions();

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.InitializeMartenWith<CatalogInitialData>();
            }

            return builder;
        }
    }
}
