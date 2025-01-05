WebApplication
    .CreateBuilder(args)
    .RegisterServices()
    .Build()
    .Configure()
    .Run();
