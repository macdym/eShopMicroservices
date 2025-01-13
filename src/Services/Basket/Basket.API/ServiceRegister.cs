using Basket.API.Data;

namespace Basket.API
{
    public static class ServiceRegister
    {
        public static WebApplicationBuilder Register(this WebApplicationBuilder builder)
        {
            var assemblyMarker = typeof(Program).Assembly;
            var connectionString = builder.Configuration.GetConnectionString("Database");

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
                opts.Connection(connectionString!);
            }).UseLightweightSessions();

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            builder.Services.AddHealthChecks()
                .AddNpgSql(connectionString!);

            return builder;
        }
    }
}
