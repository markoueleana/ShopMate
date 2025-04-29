using Discount.Grpc.Entities;
using Discount.Grpc.Services;
using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();


builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<Coupon>().Identity(c => c.ProductName);
}).UseLightweightSessions();


var app = builder.Build();
app.MapGrpcService<DiscountService>();

app.Run();
