
using Carter;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Ordering.Application;
using Ordering.Application.Orders;
using Ordering.Application.Orders.CustomerOrderEmail;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Email;

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
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService,EmailService>();
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
