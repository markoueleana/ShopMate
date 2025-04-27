
using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices() ;
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDb();
}

app.Run();
