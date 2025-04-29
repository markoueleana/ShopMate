using Cart.API.Entities;
using Carter;
using Marten;
using BuildingBlocksMessaging.Broker;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddMessageBroker(builder.Configuration, typeof(Program).Assembly);

var app = builder.Build();
app.MapCarter();
app.Run();

app.Run();
