using Catalog.API;

var builder = WebApplication
    .CreateBuilder(args)
    .RegisterServices();

var app = builder.Build();

app.MapCarter();

app.Run();
