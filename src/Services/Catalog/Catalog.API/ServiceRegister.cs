namespace Catalog.API
{
    public static class ServiceRegister
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            var assemblyMarker = typeof(Program).Assembly;
            var connectionString  = builder.Configuration.GetConnectionString(ServiceRegisterConst.DATABASE);

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

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.InitializeMartenWith<CatalogInitialData>();
            }

            builder.Services.AddHealthChecks()
                .AddNpgSql(connectionString!);

            return builder;
        }
    }
}
