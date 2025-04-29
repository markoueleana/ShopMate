using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;

namespace Ordering.Application.Orders.CustomerOrderEmail;

public class CustomerOrdersEmailJob
{
    private IOrderDbContext orderDbContext;
    private IEmailService emailService;
    public CustomerOrdersEmailJob(IOrderDbContext context, IEmailService emailService)
    {
        orderDbContext = context;
        this.emailService = emailService;
    }
    public async Task RunJob()
    {
        var customers = await orderDbContext.Customers
                     .ToListAsync();

        var toDate = DateTime.Now;
        var fromDate = toDate.AddMonths(-1);

        foreach (var customer in customers)
        {
            var orders = await orderDbContext.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == customer.Id)
                .Where(o => o.CreatedAt >= fromDate && o.CreatedAt <= toDate)
                .OrderBy(o => o.OrderName)
                .ToListAsync();

            await emailService.SendEmailAsync(
            customer.Email, 
            "Your Monthly Orders",
            $"Dear {customer.FirstName}, you placed {orders.Count} order(s) in the last month!"
            );

        }
    }
}
