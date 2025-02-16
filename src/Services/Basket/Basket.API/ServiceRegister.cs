﻿namespace Basket.API
{
    public static class ServiceRegister
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            var dbConnectionString = builder.Configuration.GetConnectionString(ServiceRegisterConst.DATABASE);
            var redisConnectionString = builder.Configuration.GetConnectionString(ServiceRegisterConst.REDIS);

            builder
                .SetApplicationServices()
                .SetDataServices(dbConnectionString!, redisConnectionString!)
                .SetDependencyInjectionServices();
            
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            builder.Services.AddHealthChecks()
                .AddNpgSql(dbConnectionString!)
                .AddRedis(redisConnectionString!);

            return builder;
        }

        private static WebApplicationBuilder SetApplicationServices(this WebApplicationBuilder builder)
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

            return  builder;
        }

        private static WebApplicationBuilder SetDataServices(this WebApplicationBuilder builder,
                                                             string dbConnectionString,
                                                             string redisConnectionString)
        {
            builder.Services.AddMarten(opts =>
            {
                opts.Connection(dbConnectionString);
            }).UseLightweightSessions();

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            return builder;
        }

        private static WebApplicationBuilder SetDependencyInjectionServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.Decorate<IBasketRepository, CashedBasketRepository>();
            builder.Services.AddTransient<IStoreBasketDiscountService, StoreBasketDiscountService>();

            return builder;
        }
    }
}
