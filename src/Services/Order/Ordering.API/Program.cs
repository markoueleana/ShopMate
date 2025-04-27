
using Carter;
using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
     .AddCarter();

var app = builder.Build();

app.MapCarter();
if (app.Environment.IsDevelopment())
{
    await app.InitializeDb();
}
app.Run();
