using Carter;
using Catalog.API.Data;
using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();

if (builder.Environment.IsDevelopment()) builder.Services.InitializeMartenWith<CatalogInitialData>();

var app = builder.Build();
app.MapCarter();
app.Run();
