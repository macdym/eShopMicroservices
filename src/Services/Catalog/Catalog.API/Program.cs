using Catalog.API;

WebApplication
    .CreateBuilder(args)
    .RegisterServices()
    .Build()
    .Configure()
    .Run();
