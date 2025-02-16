WebApplication
    .CreateBuilder(args)
    .RegisterServices()
    .Build()
    .ConfigureApp()
    .Run();
