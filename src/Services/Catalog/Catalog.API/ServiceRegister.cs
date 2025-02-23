namespace Catalog.API
{
    public static class ServiceRegister
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            var connectionString  = builder.Configuration.GetConnectionString(ServiceRegisterConst.DATABASE);

            builder
                .AddDataServices(connectionString!)
                .AddApplicationServices(connectionString!);

            return builder;
        }

        private static WebApplicationBuilder AddDataServices(this WebApplicationBuilder builder, string dbConnectionString)
        {
            builder.Services.AddMarten(opts =>
            {
                opts.Connection(dbConnectionString!);
            }).UseLightweightSessions();

            return builder;
        }

        private static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder, string dbConnectionString)
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
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.InitializeMartenWith<CatalogInitialData>();
            }
            builder.Services
                .AddHealthChecks()
                .AddNpgSql(dbConnectionString!);

            return builder;
        }
    }
}
