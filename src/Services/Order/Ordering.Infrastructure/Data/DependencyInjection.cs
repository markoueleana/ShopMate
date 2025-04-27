using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infrastructure.Data.Extensions;

namespace Ordering.Infrastructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database")!;
        services.AddDbContext<OrderDbContext>(opt => opt.UseSqlServer(connectionString));
        services.AddScoped<IOrderDbContext, OrderDbContext>();
        return services;
    }
    public static async Task InitializeDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();
        await context.Customers.AddRangeAsync(InitialData.Customers);
        await context.SaveChangesAsync();
        await context.Products.AddRangeAsync(InitialData.Products);
        await context.SaveChangesAsync();
    }


}
