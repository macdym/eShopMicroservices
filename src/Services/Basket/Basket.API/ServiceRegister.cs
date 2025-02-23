namespace Basket.API
{
    public static class ServiceRegister
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            var dbConnectionString = builder.Configuration.GetConnectionString(ServiceRegisterConst.DATABASE);
            var redisConnectionString = builder.Configuration.GetConnectionString(ServiceRegisterConst.REDIS);

            builder
                .AddDataServices(dbConnectionString!, redisConnectionString!)
                .AddApplicationServices(dbConnectionString!, redisConnectionString!)
                .AddDependencyInjectionServices();

            return builder;
        }

        private static WebApplicationBuilder AddDataServices(this WebApplicationBuilder builder,
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
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.Decorate<IBasketRepository, CashedBasketRepository>();

            return builder;
        }

        private static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder,
                                                                    string dbConnectionString,
                                                                    string redisConnectionString)
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
            builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
            {
                options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                return handler;
            });
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();
            builder.Services
                .AddHealthChecks()
                .AddNpgSql(dbConnectionString!)
                .AddRedis(redisConnectionString!);

            return  builder;
        }

        private static WebApplicationBuilder AddDependencyInjectionServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IStoreBasketDiscountService, StoreBasketDiscountService>();

            return builder;
        }
    }
}
