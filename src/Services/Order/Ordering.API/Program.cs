
using Carter;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Ordering.API;
using Ordering.Application;
using Ordering.Application.Orders;
using Ordering.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
     .AddCarter();

builder.Services.AddHangfire(x =>
    x.UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseInMemoryStorage()
);

builder.Services.AddHangfireServer();
builder.Services.AddScoped<CustomerOrdersEmailJob>();
var app = builder.Build();

app.MapCarter();
if (app.Environment.IsDevelopment())
{
    await app.InitializeDb();
}

var options = new DashboardOptions()
{
    Authorization = new[] { new MyAuthFilter() }
};
app.UseHangfireDashboard("/hangfire",options);

RecurringJob.AddOrUpdate<CustomerOrdersEmailJob>("customer-orders-email-job", x => x.RunJob(),
    Cron.Minutely); 

app.Run();

public class MyAuthFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        return true;
    }
}
