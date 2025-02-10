namespace Basket.API
{
    public static class ServiceRegister
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            var assemblyMarker = typeof(Program).Assembly;
            var dbConnectionString = builder.Configuration.GetConnectionString(ServiceRegisterConst.DATABASE);
            var redisConnectionString = builder.Configuration.GetConnectionString(ServiceRegisterConst.REDIS);

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
                opts.Connection(dbConnectionString!);
            }).UseLightweightSessions();

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.Decorate<IBasketRepository, CashedBasketRepository>();

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            builder.Services.AddHealthChecks()
                .AddNpgSql(dbConnectionString!)
                .AddRedis(redisConnectionString!);

            return builder;
        }
    }
}
